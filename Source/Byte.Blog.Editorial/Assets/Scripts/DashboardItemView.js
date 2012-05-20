window.DashboardItemView = Backbone.View.extend({

    template: null,
    tagName: 'tr',

    initialize: function (options) {
        this.template = options.template;
    },

    render: function () {

        var model = this.model.toJSON();

        var templateMarkup = $(this.template).html();
        var template = _.template(templateMarkup);
        var templatePopulated = template(model);

        $(this.el).html(templatePopulated);

        this.replacePlacholderIds();
        this.bindDeleteClicks();

        return this;
    },

    replacePlacholderIds: function () {

        $(this.el).find('.edit-link, .delete-link').each(function () {

            var el = $(this);

            var idUniquePart = el.data('id').split('/')[1];
            var href = el.attr('href').replace(/__id__/, idUniquePart);
            el.attr('href', href);

        });

    },

    bindDeleteClicks: function () {

        var self = this;

        $(this.el).find('.delete-link').each(function () {
            $(this).on('click', function (e) {
                self.onClickDelete.call(self, e);
            });
        });

    },

    onClickDelete: function (e) {

        e.preventDefault();
        e.stopPropagation();

        var anchor = $(e.target);
        var hrefToPostTo = anchor.attr('href');

        var self = this;

        $.post(hrefToPostTo)
            .success(function (data) {
                self.trigger('dashboarditem:delete', { success: true, data: data });
            })
            .error(function () {
                self.trigger('dashboarditem:delete', { success: false });
            });

    }
});