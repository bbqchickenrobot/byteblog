(function () {

    var spinnerTopBufferPixels = 20;

    window.DashboardView = Backbone.View.extend({

        pageNumber: null,
        pageSize: null,
        itemTemplate: null,

        dashboardItems: null,

        initialize: function (options) {

            this.startSpinLoader();

            this.dashboardItems = new window.FetchableCollection([], {
                fetchUrl: options.fetchUrl,
                model: options.model
            });

            this.pageNumber = options.pageNumber;
            this.pageSize = options.pageSize;
            this.itemTemplate = options.itemTemplate;

            this.fetch();
        },

        fetch: function () {

            var self = this;

            this.dashboardItems.fetch({
                data: {
                    PageNumber: this.pageNumber,
                    PageSize: this.pageSize
                },
                success: function () {
                    self.render();
                    self.stopSpinLoader();
                }
            });

        },

        startSpinLoader: function () {
            $(this.el).spin({ top: spinnerTopBufferPixels });
        },

        stopSpinLoader: function () {
            $(this.el).spin(false);
        },

        render: function () {

            var tbody = $(this.el).find('tbody');
            tbody.empty();

            var self = this;

            this.dashboardItems.each(function (item) {
                $(self.el).append(new window.DashboardItemView({
                    model: item,
                    template: self.itemTemplate
                }).render().el);
            });

            return this;
        }
    });

})();