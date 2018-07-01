var app = angular.module('ProjectList', []);

app.config(['$locationProvider', function ($locationProvider) {
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
    //$locationProvider.html5Mode(true);        

}]);



app.controller('ProjectListController', function ($scope, $location, DataService) {
    $scope.task = null;
    $scope.StatusOptions = ['To Do', 'In Progress', 'Completed', 'Backlog'];
    var currentUrl = $location.path().split("/")[3]

    DataService.getProjectTasks(currentUrl).then(function (result) {
        $scope.taskLists = result.data;
    });

    $scope.setTask = function (item) {
        //console.log(item)
        //angular.element(document.getElementById("itemDetails")).scope().item = item;
        // add some ui update like highlight
        $scope.task = item;
    };

    $scope.saveTaskDescription = function (item) {
        //db_list.push(item);
        //$rootScope.item = null;
    };

});


app.service('DataService', ['$http',
    function ($http) {
        var apiUrl = '/Tasks';

        this.getProjectTasks = function (projectId) {
            return $http.get(apiUrl + '/ProjectJson/' + projectId);
        };

        this.getTaskDescription = function (descId) {
            return $http.get(apiUrl + '/TaskDescription/' + descId);
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