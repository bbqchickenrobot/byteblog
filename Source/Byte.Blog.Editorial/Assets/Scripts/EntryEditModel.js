window.EntryEditModel = Backbone.Model.extend({

    idAttribute: 'Id',

    defaults: {
        Title: 'Untitled'
    },

    initialize: function () {

    },

    parse: function (response) {

        if (response.PublishedAtUtc) {
            response.PublishedAtUtc = DateUtility.parseFromAspNetMvcJson(response.PublishedAtUtc);
        }

        response.LastModifiedAtUtc = DateUtility.parseFromAspNetMvcJson(response.LastModifiedAtUtc);

        return response;
    }

});