window.ArrayUtility = (function () {

    return {
        isArray: function (suspect) {

            if (Object.prototype.toString.call(suspect) === '[object Array]') {
                return true;
            }

            return false;

        }
    };

})();
