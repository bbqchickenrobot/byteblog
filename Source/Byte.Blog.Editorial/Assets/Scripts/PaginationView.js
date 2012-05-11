window.PaginationView = Backbone.View.extend({

    currentPageNumber: null,
    totalItems: null,
    itemsPerPage: 20,

    minPageNumber: null,
    maxPageNumber: null,

    listEl: null,

    initialize: function (options) {

        this.currentPageNumber = options.currentPageNumber;
        this.totalItems = options.totalItems;
        this.itemsPerPage = options.itemsPerPage;

    },

    render: function () {

        this.calculateMinPageNumber();
        this.calculateMaxPageNumber();

        this.setupPaginationListElement();

        this.renderPageLinks();
        this.renderPreviousLink();
        this.renderNextLink();

        return this;

    },

    calculateMinPageNumber: function () {

        var calculatedMinPageNumber = this.currentPageNumber - PaginationView.pagesAwayFromCurrentToDisplay;

        var minPossiblePageNumber = 1;

        if (calculatedMinPageNumber < minPossiblePageNumber) {
            this.minPageNumber = minPossiblePageNumber;
        } else {
            this.minPageNumber = calculatedMinPageNumber;
        }
    },

    calculateMaxPageNumber: function () {

        var calculatedMaxPageNumber = this.currentPageNumber + PaginationView.pagesAwayFromCurrentToDisplay;

        var maxPossiblePageNumber = Math.ceil(this.totalItems / this.itemsPerPage);

        if (calculatedMaxPageNumber > maxPossiblePageNumber) {
            this.maxPageNumber = maxPossiblePageNumber;
        } else {
            this.maxPageNumber = calculatedMaxPageNumber;
        }
    },

    setupPaginationListElement: function () {

        this.$el.empty();
        this.listEl = $('<ul />').appendTo(this.$el);

    },

    renderPageLinks: function () {

        var self = this;

        var minPageNum = parseInt(this.minPageNumber);
        var maxPageNum = parseInt(this.maxPageNumber) + 1;

        _.each(_.range(minPageNum, maxPageNum), function (pageNumberForLink) {

            var listItem = $('<li />')
                .appendTo(self.listEl);

            if (self.currentPageNumber == pageNumberForLink) {
                listItem.addClass('active');
            }

            $('<a />')
                .attr('href', '#')
                .data('pagination', pageNumberForLink)
                .appendTo(listItem)
                .text(pageNumberForLink)
                .on('click', function (e) {
                    self.onClickPaginationLink.call(self, e);
                });
        });
    },

    renderPreviousLink: function () {

        var pageNumberForLink = this.currentPageNumber - 1;

        var listItem = $('<li />')
            .prependTo(this.listEl);

        if (pageNumberForLink < this.minPageNumber) {
            listItem.addClass('disabled');
        }

        var self = this;

        $('<a />')
            .attr('href', '#')
            .data('pagination', pageNumberForLink)
            .appendTo(listItem)
            .text('«')
            .on('click', function (e) {
                self.onClickPaginationLink.call(self, e);
            });

    },

    renderNextLink: function () {

        var pageNumberForLink = this.currentPageNumber + 1;

        var listItem = $('<li />')
            .appendTo(this.listEl);

        if (pageNumberForLink > this.maxPageNumber) {
            listItem.addClass('disabled');
        }

        var self = this;

        $('<a />')
            .attr('href', '#')
            .attr('data-pagination', pageNumberForLink)
            .appendTo(listItem)
            .text('»')
            .on('click', function (e) {
                self.onClickPaginationLink.call(self, e);
            });

    },

    onClickPaginationLink: function (e) {

        e.preventDefault();

        var pageToGoTo = $(e.target).data('pagination');

        if (pageToGoTo < this.minPageNumber || pageToGoTo > this.maxPageNumber) {
            return;
        }

        this.currentPageNumber = pageToGoTo;
        
        this.trigger('pagination:changed', this.currentPageNumber, this.itemsPerPage);

    }
}, {

    pagesAwayFromCurrentToDisplay: 5

});