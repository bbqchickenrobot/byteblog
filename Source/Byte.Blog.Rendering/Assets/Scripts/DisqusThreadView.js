window.DisqusThreadView = Backbone.View.extend({

    disqus_shortname: null,
    disqus_identifier: null,
    disqus_url: null,

    initialize: function (options) {

        this.disqus_shortname = this.$el.data('disqus-shortname');
        this.disqus_identifier = this.$el.data('disqus-identifier');
        this.disqus_url = this.$el.data('disqus-url');

        this.exposeDisqusVariables();
        this.activateDisqusThread();
    },

    exposeDisqusVariables: function () {

        window.disqus_shortname = this.disqus_shortname;
        window.disqus_identifier = this.disqus_identifier;
        window.disqus_url = this.disqus_url;

    },

    activateDisqusThread: function () {

        var disqusScript = document.createElement('script');
        disqusScript.type = 'text/javascript';
        disqusScript.async = true;
        disqusScript.src = 'http://' + window.disqus_shortname + '.disqus.com/embed.js';
        (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(disqusScript);

    }
});