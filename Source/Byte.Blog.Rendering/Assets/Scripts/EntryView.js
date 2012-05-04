window.EntryView = Backbone.View.extend({

    pageSlug: null,
    entrySlug: null,
    canonicalUrl: null,

    initialize: function (options) {

        this.pageSlug = options.pageSlug;
        this.entrySlug = options.entrySlug;
        this.canonicalUrl = options.canonicalUrl;

        this.setDisqusVariables();
    },

    setDisqusVariables: function () {

        window.disqus_category_id = this.pageSlug;
        window.disqus_identifier = this.entrySlug;
        window.disqus_url = this.canonicalUrl;

    }

});