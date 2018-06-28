var app = angular.module('ProjectList', []);

app.controller('ListController', function ($scope, DataService) {
    // Default data (can be loaded from a database)
    $scope.records = [
        { state: 'CA', price: 22, tax: 5, include: false },
        { state: 'MA', price: 32, tax: 8, include: false }
    ];

    $scope.editItem = function (item) {
        console.log(item)
        angular.element(document.getElementById("itemDetails")).scope().item = item;
    };

});


app.controller('ItemController', function ($scope, DataService) {
    $scope.saveItem = function (item) {
        //db_list.push(item);
        //$rootScope.item = null;
    };
});



app.service('DataService', function () {
    var myList = [
        { state: 'CA', price: 22, tax: 5, include: false },
        { state: 'MA', price: 32, tax: 8, include: false }
    ];


    var addList = function (newObj) {
        myList.push(newObj);
    }

    var getList = function () {
        return myList;
    }

    return {
        addList: addList,
        getList: getList
    };

});