(function () {

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

            return this;
        },

        replacePlacholderIds: function () {

            var editLink = $(this.el).find('.edit-link');
            if (editLink.length) {
                var idUniquePart = editLink.data('id').split('/')[1];
                var href = editLink.attr('href').replace(/__id__/, idUniquePart);
                editLink.attr('href', href);
            }

        }
    });

})();