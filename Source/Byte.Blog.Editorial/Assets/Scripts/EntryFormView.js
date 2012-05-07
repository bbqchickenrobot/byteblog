window.EntryFormView = Backbone.View.extend({

    events: {
        'click #Save': 'save',
        'click #Delete': 'savedelete',
        'click #Undelete': 'undelete'
    },

    saveUrl: null,
    dateTimePicker: null,

    initialize: function (options) {

        this.saveUrl = options.saveUrl;

        this.model = new window.EntryEditModel({
            Id: options.modelId
        });

        this.model.bind('change', this.render, this);

        this.initializeModel();
        this.initializeDateTimePicker();
        this.initializeButtons();
        this.render();
    },

    initializeModel: function () {
        var attrsFromForm = this.getModelAttributesFromForm();
        this.model.set(attrsFromForm, { silent: true });
    },

    initializeDateTimePicker: function () {

        this.dateTimePicker = new window.DateTimePickerView({
            el: this.el,
            currentDateTime: this.model.get('PublishedAtUtc') || new Date()
        });

    },

    initializeButtons: function () {

        if (this.model.get('Deleted') === 'True') {
            $('#Undelete').removeClass('hide');
        } else {
            $('#Delete').removeClass('hide');
        }

    },

    save: function () {

        this.disableButton($('#Save'), 'Saving...');

        var self = this;

        this.saveData({
            success: function (model, response) {
                self.enableButton($('#Save'), 'Save');
                self.trigger('route:update', model.get('Id').split('/')[1]);
            },
            error: function (model, response) {
                self.enableButton($('#Save'), 'Save');
            }
        });

    },

    savedelete: function () {

        this.disableButton($('#Delete'), 'Deleting...');

        $('#Deleted').val('True');

        var self = this;

        this.saveData({
            success: function (model, response) {
                self.enableButton($('#Delete'), 'Delete');
                $('#Delete').addClass('hide');
                $('#Undelete').removeClass('hide');
            },
            error: function (model, response) {
                //TODO: display an error
                self.enableButton($('#Delete'), 'Delete');
            }
        });

    },

    undelete: function () {

        this.disableButton($('#Undelete'), 'Undeleting...');

        $('#Deleted').val('False');

        var self = this;

        this.saveData({
            success: function (model, response) {
                self.enableButton($('#Undelete'), 'Undelete');
                $('#Undelete').addClass('hide');
                $('#Delete').removeClass('hide');
            },
            error: function (model, response) {
                //TODO: display an error
                self.enableButton($('#Undelete'), 'Undelete');
            }
        });

    },

    render: function () {

        var attributes = this.model.toJSON();

        _.each(attributes, function (value, key) {
            $('#' + key).val(value);
        });

    },

    disableButton: function (el, pendingText) {
        $(el).html(pendingText).attr('disabled', 'disabled');
    },

    enableButton: function (el, text) {
        $(el).removeAttr('disabled').html(text);
    },

    saveData: function (options) {

        var attrs = this.getModelAttributesFromForm();

        this.model.save(attrs, {
            url: this.saveUrl,
            success: options.success,
            error: options.error
        });
    },

    getModelAttributesFromForm: function () {

        var attrsFound = $(this.el).serializeObject();

        console.log('attrsFound', attrsFound);

        //ASP.NET MVC inserts a hidden 'false' input for each checkbox.
        //Without it, the input name for the checkbox would not submit a 'false' value when unchecked.
        //When checked, the model binder treats ['true', 'false'] as 'true' server side. 
        //In order to use serializeObject() for convenient model binding, we need to explicitly correct its output
        //Otherwise the 2nd value that gets hit in ['true','false'] will set the property false always.
        $(this.el).find('input:hidden').each(function (idx, el) {
            if ($('input:checkbox[name="' + el.name + '"]').is(':checked')) {
                attrsFound[el.name] = true;
            }
        });

        if (attrsFound.PublishedAtUtc) {
            attrsFound.PublishedAtUtc = new Date(attrsFound.PublishedAtUtc);
        } else {
            attrsFound.PublishedAtUtc = new Date();
        }

        return attrsFound;
    }
});