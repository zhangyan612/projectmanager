'use strict';

var app = angular.module('ProjectList', ['ngSanitize', 'ui.select']);

app.config(['$locationProvider', function ($locationProvider) {
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
}]);


/**
 * AngularJS default filter with the following expression:
 * "person in people | filter: {name: $select.search, age: $select.search}"
 * performs a AND between 'name: $select.search' and 'age: $select.search'.
 * We want to perform a OR.
 */

app.filter('propsFilter', function () {
    return function (items, props) {
        var out = [];

        if (angular.isArray(items)) {
            items.forEach(function (item) {
                var itemMatches = false;

                var keys = Object.keys(props);
                for (var i = 0; i < keys.length; i++) {
                    var prop = keys[i];
                    var text = props[prop].toLowerCase();
                    if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                        itemMatches = true;
                        break;
                    }
                }

                if (itemMatches) {
                    out.push(item);
                }
            });
        } else {
            // Let the output be the input untouched
            out = items;
        }

        return out;
    }
});



app.controller('ProjectListController', function ($scope, $location, DataService) {
    var currentUrl = $location.path().split("/")[3]
    $scope.task = null;
    $scope.StatusOptions = ['To Do', 'In Progress', 'Completed', 'Backlog'];
    $scope.descEditMode = true;
    $scope.TaskDescription = null;

    $scope.getCurrentId = function () {
        return currentUrl;
    }


    DataService.getProjectTasks(currentUrl).then(function (result) {
        $scope.taskLists = result.data;
    });

    $scope.setTask = function (item) {
        //console.log(item)
        //angular.element(document.getElementById("itemDetails")).scope().item = item;
        // add some ui update like highlight
        $scope.task = item;
        if (item) {
            var descId = item.DescriptionId;
            console.log(descId);
            if (descId) {
                DataService.getTaskDescription(descId).then(function (result) {
                    $scope.TaskDescription = result.data;
                    $scope.descEditMode = false;
                });
            } else {
                $scope.TaskDescription = null;
                $scope.descEditMode = true;
            }
        }
    };

    $scope.saveTaskDescription = function () {
        console.log($scope.task);
        console.log($scope.TaskDescription.Description);

        // save item to db 
        DataService.saveTaskDescription($scope.task.Id, $scope.TaskDescription.Description);

        $scope.descEditMode = false;
    };

    $scope.EditTaskDescription = function () {
        $scope.descEditMode = true;
    };

    $scope.ModelChanged = function () {
        // commom method for any update operation
        console.log($scope.task);
        //post entire task to save

    };

    //$scope.$watch("task", function (newVal, oldVal) {
    //    if (newVal != oldVal) {
    //        $scope.msg = 'task is changed, new model:';
    //        console.log($scope.msg);
    //        console.log(newVal);

    //    }
    //}, true);

    //About assigning to person
    $scope.person = {};
    $scope.people = [
        { Id: 1, FullName: 'Adam', Email: 'adam@email.com', age: 10 },
        { Id: 2, FullName: 'Amalie', Email: 'amalie@email.com', age: 12 },
        { Id: 3, FullName: 'Wladimir', Email: 'wladimir@email.com', age: 30 },
        { Id: 4, FullName: 'Samantha', Email: 'samantha@email.com', age: 31 },
        { Id: 5, FullName: 'Estefanía', Email: 'estefanía@email.com', age: 16 },
        { Id: 6, FullName: 'Natasha', Email: 'natasha@email.com', age: 54 },
        { Id: 7, FullName: 'Nicole', Email: 'nicole@email.com', age: 43 },
        { Id: 8, FullName: 'Adrian', Email: 'adrian@email.com', age: 21 }
    ];

});


app.service('DataService', ['$http',
    function ($http) {
        var apiUrl = '/Tasks';

        this.getProjectTasks = function (projectId) {
            return $http.get(apiUrl + '/GetTasks/' + projectId);
        };

        this.getTaskDescription = function (descId) {
            return $http.get(apiUrl + '/TaskDescription/' + descId);
        };

        this.saveTaskDescription = function (id, desc) {
            return $http.post(apiUrl + '/SaveDescription?id=' + id + '&desc=' + desc);
        };

        this.getTaskUsers = function () {
            return $http.post(apiUrl + '/UserList');
        };

}]);

//app.service('DataService', function () {
//    var ProjectTasks = [
//        { state: 'CA', price: 22, tax: 5, include: false },
//        { state: 'MA', price: 32, tax: 8, include: false }
//    ];

//    var getProjectTasks = function (projectId) {
//        return $http({
//            method: 'GET',
//            url: '/Tasks/ProjectJson/' + projectId,
//        });
//    }

//    var addList = function (newObj) {
//        myList.push(newObj);
//    }

//    var getList = function () {
//        return myList;
//    }

//    return {
//        addList: addList,
//        getList: getList,
//        ProjectTasks,
//        getProjectTasks: getProjectTasks,
//    };

//});