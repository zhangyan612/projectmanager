var app = angular.module('ProjectList', []);

app.config(['$locationProvider', function ($locationProvider) {
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
}]);



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

    $scope.statusChanged = function (status) {
        console.log($scope.task);
        console.log(status);
        //post entire task to save

    }

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