window.FetchableCollection = Backbone.Collection.extend({

    fetchUrl: null,

    initialize: function (models, options) {
        this.fetchUrl = options.fetchUrl;
        this.model = options.model;
    },

    url: function () {
        return this.fetchUrl;
    }
});