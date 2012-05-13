window.PageView = Backbone.View.extend({

    disqus_shortname: null,

    initialize: function (options) {

        this.disqus_shortname = this.$el.data('disqus-shortname');

        this.exposeDisqusVariables();
        this.activateDisqusCount();
    },

    exposeDisqusVariables: function () {
        window.disqus_shortname = this.disqus_shortname;
    },

    activateDisqusCount: function () {

        var disqusScript = document.createElement('script');
        disqusScript.type = 'text/javascript';
        disqusScript.async = true;
        disqusScript.src = 'http://' + window.disqus_shortname + '.disqus.com/count.js';
        (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(disqusScript);

    }
});