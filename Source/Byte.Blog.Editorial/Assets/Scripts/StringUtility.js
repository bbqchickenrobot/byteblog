window.StringUtility = (function () {

    var slashEndingRegex = /\/$/;

    return {
        trimTrailingSlash: function (str) {
            return str.replace(slashEndingRegex, "");
        }
    };

})();
