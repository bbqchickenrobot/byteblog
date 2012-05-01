window.EntryFormRouter = Backbone.Router.extend({

    routes: {
        "index": "search"
    },

    entryEditUrlBase: null,

    initialize: function (options) {

        this.entryEditUrlBase = options.entryEditUrlBase;

        Backbone.history.start({ pushState: true });

        var self = this;

        options.view.bind('route:update', function (entryId) {
            console.log('going to navigate with', entryId);

            self.navigate(self.entryEditUrlBase + entryId);
        });
    }

});
