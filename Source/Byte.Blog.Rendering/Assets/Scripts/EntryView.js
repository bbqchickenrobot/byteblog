window.EntryView = Backbone.View.extend({

    developer: null,
    pageSlug: null,
    entrySlug: null,
    //canonicalUrl: null,

    initialize: function (options) {

        this.pageSlug = this.$el.data('page-slug');
        this.entrySlug = this.$el.data('entry-slug');
        //this.canonicalUrl = this.$el.data('canonical-url');

        this.setDisqusVariables();
        this.activateDisqus();
    },

    setDisqusVariables: function () {

        window.disqus_shortname = 'benlakey';
        //window.disqus_category_id = this.pageSlug;
        window.disqus_identifier = this.entrySlug;
        //window.disqus_url = this.canonicalUrl;

    },

    activateDisqus: function () {

        var dsq = document.createElement('script');
        dsq.type = 'text/javascript';
        dsq.async = true;
        dsq.src = 'http://' + window.disqus_shortname + '.disqus.com/embed.js';
        (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(dsq);

    }
});