window.PaginationView = Backbone.View.extend({

    //static
    pagesAwayFromCurrentToDisplay: 5,

    //things coming from outside
    currentPageNumber: null,
    totalItems: null,
    itemsPerPage: 20,

    //calculated
    minPageNumber: null,
    maxPageNumber: null,

    //refs to hang on to
    listEl: null,

    initialize: function (options) {

        this.currentPageNumber = options.currentPageNumber;
        this.totalItems = options.totalItems;
        this.itemsPerPage = options.itemsPerPage;

        this.render();

    },

    render: function () {

        console.log('rendering pagination');

        this.calculateMinPageNumber();
        this.calculateMaxPageNumber();

        this.listEl = $('<ul />').appendTo(this.$el);

        this.renderPageLinks();
        this.renderPreviousLink();
        this.renderNextLink();

        return this;

    },

    calculateMinPageNumber: function () {

        var calculatedMinPageNumber = this.currentPageNumber - this.pagesAwayFromCurrentToDisplay;

        console.log('calculatedMinPageNumber', calculatedMinPageNumber);

        var minPossiblePageNumber = 0;

        console.log('minPossiblePageNumber', minPossiblePageNumber);

        if (calculatedMinPageNumber < minPossiblePageNumber) {

            this.minPageNumber = minPossiblePageNumber;

            console.log('calculatedMinPageNumber was less than minPossiblePageNumber, so set minPageNumber to: ' + this.minPageNumber);

        } else {
            this.minPageNumber = calculatedMinPageNumber;

            console.log('calculatedMinPageNumber was acceptable, so set minPageNumber to: ' + this.minPageNumber);
        }
    },

    calculateMaxPageNumber: function () {
        var calculatedMaxPageNumber = this.currentPageNumber + this.pagesAwayFromCurrentToDisplay;

        console.log('calculatedMaxPageNumber', calculatedMaxPageNumber);

        var maxPossiblePageNumber = this.totalItems / this.itemsPerPage;

        console.log('maxPossiblePageNumber', maxPossiblePageNumber);

        if (calculatedMaxPageNumber > maxPossiblePageNumber) {

            this.maxPageNumber = maxPossiblePageNumber;

            console.log('calculatedMaxPageNumber was greater than maxPossiblePageNumber, so set maxPageNumber to: ' + this.maxPageNumber);

        } else {

            this.maxPageNumber = calculatedMaxPageNumber;

            console.log('calculatedMaxPageNumber was acceptable, so set maxPageNumber to: ' + this.maxPageNumber);

        }
    },

    renderPageLinks: function () {

        var self = this;

        _.each(_.range(this.minPageNumber, this.maxPageNumber), function (pageNumberForLink) {

            console.log('in renderPageLinks at index: ' + pageNumberForLink);

            var listItem = $('<li />')
                .appendTo(self.listEl);

            $('<a />')
                .attr('href', '#')
                .data('pagination', pageNumberForLink)
                .appendTo(listItem)
                .text(pageNumberForLink)
                .on('click', function (e) {
                    self.onClickPaginationLink(e);
                });
        });
    },

    renderPreviousLink: function () {

        var self = this;

        var listItem = $('<li />')
            .prependTo(self.listEl);

        var pageNumberForLink = this.currentPageNumber - 1;

        console.log('in renderPreviousLink, and using pageNumberForLink: ' + pageNumberForLink);

        $('<a />')
            .attr('href', '#')
            .data('pagination', pageNumberForLink)
            .appendTo(listItem)
            .text('«')
            .on('click', function (e) {
                self.onClickPaginationLink(e);
            });

    },

    renderNextLink: function () {

        var self = this;

        var listItem = $('<li />')
            .appendTo(self.listEl);

        var pageNumberForLink = this.currentPageNumber + 1;

        console.log('in renderingNextLink, and using pageNumberForLink: ' + pageNumberForLink);

        $('<a />')
            .attr('href', '#')
            .attr('data-pagination', pageNumberForLink)
            .appendTo(listItem)
            .text('»')
            .on('click', function (e) {
                self.onClickPaginationLink(e);
            });

    },

    onClickPaginationLink: function (e) {

        e.preventDefault();

        var pageToGoTo = $(e.target).data('pagination');

        //ensure falls in range

        console.log('onClickPaginationLink pageToGoTo', pageToGoTo);



    }
});