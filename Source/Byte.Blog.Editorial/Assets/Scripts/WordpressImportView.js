window.WordpressImportview = Backbone.View.extend({

    events: {
        'click #import-wordpress': 'submit'
    },

    saveUrl: null,
    ajaxUpload: null,

    initialize: function (options) {

        this.saveUrl = this.$el.data('save-url');

        this.initializeUploader();

    },

    initializeUploader: function () {

        var self = this;

        this.ajaxUpload = new AjaxUpload('choose-file', {
            action: this.saveUrl,
            name: 'wordpressXml',
            autoSubmit: false,
            responseType: 'json',
            onComplete: function () {
                self.onAjaxUploadComplete.apply(self, arguments);
            },
            onChange: function () {
                self.onFileChosen.apply(self, arguments);
            }
        });

    },

    onFileChosen: function (file, extension) {

        this.$el.find('#filename').text(file);
        this.$el.find('#import-wordpress').removeClass('hide');
        this.$el.find('#file-chooser').addClass('hide');

    },

    onAjaxUploadComplete: function (file, response) {

        this.stopSpinLoader();
        this.$el.find('#file-importer').addClass('hide');

        var responseMessageElement = this.$el.find('.response-message');

        responseMessageElement
            .removeClass('alert-success')
            .removeClass('alert-error');

        if (response.error) {
            responseMessageElement
                        .addClass('alert-error')
                        .text(response.error);
        } else {

            responseMessageElement
                        .addClass('alert-success')
                        .addClass('span2')
                        .html('<p>Success! Note: Entries imported are in Draft state so you can review them before publishing.</p>' + 
                            '<ul>' +
                                '<li>' + response.EntriesCount + ' entries. </li>' +
                                '<li>' + response.PagesCount + ' pages. </li>' +
                                '<li>' + response.TagsCount + ' tags. </li>' +
                            '</ul>');
        }

    },

    startSpinLoader: function () {
        $(this.el).spin();
    },

    stopSpinLoader: function () {
        $(this.el).spin(false);
    },

    submit: function () {

        this.startSpinLoader();
        this.ajaxUpload.submit();

    }
});