window.PageEditModel = Backbone.Model.extend({

    idAttribute: 'Id',

    defaults: {
        Title: 'Untitled'
    },

    initialize: function () {

    },

    parse: function (response) {

        response.LastModifiedAtUtc = DateUtility.parseFromAspNetMvcJson(response.LastModifiedAtUtc);

        return response;
    }

});