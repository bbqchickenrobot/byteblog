(function () {

    var spinnerTopBufferPixels = 100;

    window.DashboardView = Backbone.View.extend({

        itemTemplate: null,
        dashboardItems: null,
        paginationView: null,

        initialize: function (options) {

            this.dashboardItems = new window.FetchableCollection([], {
                fetchUrl: options.fetchUrl,
                model: options.model
            });

            this.itemTemplate = options.itemTemplate;

            this.setupPagination();
        },

        setupPagination: function () {

            var pageNumber = $('#PageNumber').val();
            var pageSize = $('#PageSize').val();
            var totalItems = $('#TotalItems').val();

            var self = this;

            this.paginationView = new window.PaginationView({
                el: $('.dashboard-pagination'),
                currentPageNumber: pageNumber,
                itemsPerPage: pageSize,
                totalItems: totalItems
            }).bind('pagination:changed', function (currentPageNumber, currentPageSize) {
                self.fetch(currentPageNumber, currentPageSize);
            }).trigger('pagination:changed', pageNumber, pageSize);

        },

        fetch: function (pageNumber, pageSize) {

            this.startSpinLoader();

            var self = this;

            this.dashboardItems.fetch({
                data: {
                    PageNumber: pageNumber,
                    PageSize: pageSize
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

                var newItem = new window.DashboardItemView({
                    model: item,
                    template: self.itemTemplate
                }).render();

                $(self.el).find('.dashboard-table').append(newItem.el);

                newItem.bind('dashboarditem:delete', function (e) {
                    self.onDashboardItemDeletedEvent.call(self, e);
                });

            });

            this.paginationView.render();

            return this;
        },

        onDashboardItemDeletedEvent: function (e) {

            var actionMessageText = '';
            var actionMessageTextAccentClass = '';

            if (e && e.success) {
                actionMessageText = 'Deleted \'' + e.data.Title + '\'!';
                actionMessageTextAccentClass = 'alert-success';
                var pageSize = $('#PageSize').val();
                this.fetch(1, pageSize);
            } else {
                actionMessageText = 'Failed to delete entry!';
                actionMessageTextAccentClass = 'alert-error';
            }

            $('#action-message')
                .removeClass('alert-success alert-error')
                .addClass(actionMessageTextAccentClass)
                .fadeIn()
                .removeClass('hide')
                .text(actionMessageText)
                .delay(4000)
                .fadeOut();
        }
    });

})();