window.DateUtility = (function () {

    var epochIntRadix = 10;

    return {

        parseFromAspNetMvcJson: function (input) {

            var stripped = input.replace("/Date(", "").replace(")/", "");
            var millisSinceEpochUtc = parseInt(stripped, epochIntRadix);

            return new Date(millisSinceEpochUtc);

        },

        getDateTimeParts: function (date) {

            if (!date) {
                return {
                    datePart: null,
                    timePart: null,
                    combined: null
                };
            }

            var meridiem = "AM";

            var hours = date.getHours();
            if (hours > 12) {
                hours = hours - 12;
                meridiem = "PM";
            }

            var minutes = date.getMinutes();
            if (minutes < 10) {
                minutes = '0' + minutes;
            }

            var month = date.getMonth() + 1;

            var datePart = month + '/' + date.getDate() + '/' + date.getFullYear();
            var timePart = hours + ':' + minutes + ' ' + meridiem;

            return {
                datePart: datePart,
                timePart: timePart,
                combined: datePart + ' ' + timePart
            };

        }
    };

})();
