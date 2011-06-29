﻿// Knockout JS jQuery UI Sortable list binding 0.1
// Author: (c) Ivan Zlatev - http://ivanz.com
// Source Code: http://github.com/ivanz/knockout.jQueryUI-sortable.js
// License: MIT (http://www.opensource.org/licenses/mit-license.php)

$(function() {
    ko.bindingHandlers.sortableList = {
        _dataItemKey: "data-ko-collectionItem",

        // Detects when an HTML from a JQuery UI Sortabe has been moved
        // and applies the same move to the ViewModel collection item
        init: function (element, valueAccessor, allBindingsAccessor) {
            $(element).sortable({
                update: function (event, ui) {
                    var newIndex = $(element).children().index(ui.item);

                    var collection = ko.utils.unwrapObservable(valueAccessor());
                    var collectionItem = $(ui.item).data(ko.bindingHandlers.sortableList._dataItemKey);
                    var oldIndex = collection.indexOf(collectionItem);

                    // move the item from the old index to the new index in the collection.
                    collection.splice(oldIndex, 1);
                    collection.splice(newIndex, 0, collectionItem);
                    valueAccessor().valueHasMutated();
                }
            });
        }
    };

    ko.bindingHandlers.sortableItem = {
        // Uses Jquery.data() to associate the collection item object
        // with the HTML element.
        init: function (element, valueAccessor, allBindingsAccessor) {
            var collectionItem = ko.utils.unwrapObservable(valueAccessor());
            $(element).data(ko.bindingHandlers.sortableList._dataItemKey, collectionItem);
        }
    };
});
