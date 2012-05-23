window.EntryView = Backbone.View.extend({

    publishedAtUtc: null,

    initialize: function (options) {

        this.initializeDate();
        this.render();

    },

    initializeDate: function () {

        var publishedAtUtcStr = this.$el.data('published-at-utc');
        this.publishedAtUtc = new Date(publishedAtUtcStr);
    },

    render: function () {

        var publishedAtLocalStr =
            'Posted on ' +
            this.publishedAtUtc.toLocaleDateString() +
            ' ' +
            this.publishedAtUtc.toLocaleTimeString();

        this.$el
            .find('.published-at-utc')
            .text(publishedAtLocalStr);
    }

}, {

    initializeAll: function () {

        $('.entry').each(function (idx, el) {

            var entry = new window.EntryView({ el: el });

        });

    }
});