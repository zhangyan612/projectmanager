var app = angular.module('ProjectList', []);

app.config(['$locationProvider', function ($locationProvider) {
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
    //$locationProvider.html5Mode(true);        

}]);



app.controller('ListController', function ($scope, DataService, $location) {
    var currentUrl = $location.path().split("/")[3]

    DataService.getProjectTasks(currentUrl).then(function (result) {

        $scope.taskLists = result.data;
    });


    //DataService.getProjectTasks().then(function (dataResponse) {
    //    $scope.records = dataResponse;
    //});


    $scope.editItem = function (item) {
        console.log(item)
        angular.element(document.getElementById("itemDetails")).scope().item = item;
    };

});


app.controller('ItemController', function ($scope, DataService) {

    $scope.StatusOptions = ['To Do', 'In Progress', 'Completed', 'Backlog'];

    $scope.saveItem = function (item) {
        //db_list.push(item);
        //$rootScope.item = null;
    };



});


app.service('DataService', ['$http',
    function ($http) {
        var apiUrl = '/Tasks/ProjectJson/';

        this.getProjectTasks = function (projectId) {
            return $http.get(apiUrl + projectId);
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