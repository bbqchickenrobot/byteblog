window.DateTimePickerView = Backbone.View.extend({

    currentDateTime: null,

    events: {
        'changeDate .date-picker': 'change',
        'change .time-picker': 'change'
    },

    initialize: function (options) {

        this.currentDateTime = options.currentDateTime || new Date();

        this.initializeDatePicker();
        this.initializeTimePicker();

        this.change({ date: this.currentDateTime });
    },

    initializeDatePicker: function () {

        var dateParts = DateUtility.getDateTimeParts(this.currentDateTime);

        var datePicker = $(this.el).find('.date-picker');

        datePicker
            .attr('data-date', dateParts.datePart)
            .val(dateParts.datePart)
            .datepicker();

    },

    initializeTimePicker: function () {

        var dateParts = DateUtility.getDateTimeParts(this.currentDateTime);

        var timePicker = $(this.el).find('.time-picker');

        timePicker
            .val(dateParts.timePart)
            .timePicker({
                show24Hours: false,
                separator: ':',
                step: 15
            });

    },

    change: function (e) {

        var newDate = e.date || this.currentDateTime;
        var newTime = $.timePicker('.time-picker').getTime();

        this.currentDateTime.setMonth(newDate.getMonth());
        this.currentDateTime.setDate(newDate.getDate());
        this.currentDateTime.setFullYear(newDate.getFullYear());

        this.currentDateTime.setHours(newTime.getHours());
        this.currentDateTime.setMinutes(newTime.getMinutes());

        this.render();

    },

    render: function () {

        $('.datepicker').hide();

        var hiddenEl = $(this.el).find('.hidden-datetime');
        hiddenEl.val(this.currentDateTime);

    }


});