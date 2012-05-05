window.WordpressImportview = Backbone.View.extend({

    saveUrl: null,

    initialize: function (options) {

        this.saveUrl = this.$el.data('save-url');

        this.initializeUploader();

    },

    initializeUploader: function () {

        var responseMessageElement = this.$el.find('.response-message');

        responseMessageElement
            .removeClass('alert-success')
            .removeClass('alert-error');

        var uploader = new AjaxUpload('import-button', {
            action: this.saveUrl,
            name: 'wordpressXml',
            autoSubmit: true,
            responseType: 'json',
            onComplete: function (file, response) {
                if (response.error) {
                    responseMessageElement
                        .addClass('alert-error')
                        .text(response.error);
                } else {
                    responseMessageElement
                        .addClass('alert-success')
                        .text('Success! ' +
                            response.EntriesCount + ' entries. ' +
                            response.PagesCount + ' pages. ' +
                            response.TagsCount + ' tags.');
                }
            }
        });

    }
});