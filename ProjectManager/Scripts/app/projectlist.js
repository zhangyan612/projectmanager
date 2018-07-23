'use strict';

var app = angular.module('ProjectList', ['ngSanitize', 'ui.select', 'angularTrix']);

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

function containsObject(obj, list) {
    var i;
    for (i = 0; i < list.length; i++) {
        if (list[i] === obj) {
            return true;
        }
    }

    return false;
}




app.controller('ProjectListController', function ($scope, $location, DataService) {
    var currentUrl = $location.path().split("/")[3]

    $scope.sortType = 'Id'; // set the default sort type
    $scope.sortReverse = false;  // set the default sort order
    $scope.searchTask = '';     // set the default search/filter term
    $scope.task = null;
    $scope.StatusOptions = ['To Do', 'In Progress', 'Completed', 'Backlog'];
    $scope.descEditMode = true;
    $scope.TaskDescription = null;
    $scope.addPersonButton = true;
    $scope.person = {};
    $scope.projectUsers = [
        { Id: 1, UserProfileId: "u33433", FullName: 'Adam', Email: 'adam@email.com', PlannedHours: 10 },
        { Id: 2, UserProfileId: "u3347873", FullName: 'Amalie', Email: 'amalie@email.com', PlannedHours: 12 },
        { Id: 3, UserProfileId: "u3343333", FullName: 'Wladimir', Email: 'wladimir@email.com', PlannedHours: 30 },
        { Id: 4, UserProfileId: "u334223", FullName: 'Samantha', Email: 'samantha@email.com', PlannedHours: 31 },
        { Id: 5, UserProfileId: "u33442", FullName: 'Estefanía', Email: 'estefanía@email.com', PlannedHours: 16 },
        { Id: 6, UserProfileId: "u432433", FullName: 'Natasha', Email: 'natasha@email.com', PlannedHours: 54 },
        { Id: 7, UserProfileId: "u46433", FullName: 'Nicole', Email: 'nicole@email.com', PlannedHours: 43 },
        { Id: 8, UserProfileId: "u655433", FullName: 'Adrian', Email: 'adrian@email.com', PlannedHours: 21 }
    ];

    $scope.getCurrentId = function () {
        return currentUrl;
    }

    DataService.getProjectTasks(currentUrl).then(function (result) {
        console.log(result);
        $scope.taskLists = result.data;
    });

    $scope.setTask = function (item) {
        //console.log(item)
        //angular.element(document.getElementById("itemDetails")).scope().item = item;
        // add some ui update like highlight
        $scope.task = item;
        if (item) {
            var descId = item.DescriptionId;
            if (descId) {
                DataService.getTaskDescription(descId).then(function (result) {
                    console.log(result)
                    if (result.status == 200) {
                        $scope.TaskDescription = result.data;
                        $scope.descEditMode = false;
                    } else {
                        alert("Error saving note");
                    }
                });
            } else {
                $scope.TaskDescription = null;
                $scope.descEditMode = true;
            }

            if (item.AssignedUserList === undefined || item.AssignedUserList.length == 0) {
                console.log(item.AssignedUserList)
                $scope.addPersonButton = false;
            }
        }
    };

    $scope.saveTaskDescription = function () {
        console.log($scope.task);
        console.log($scope.TaskDescription.Description);

        // save item to db 
        //var htmlDesc = $sanitize($scope.TaskDescription.Description);

        DataService.saveTaskDescription($scope.task.Id, $scope.TaskDescription.Description).then(function (result) {
            console.log(result);
            $scope.task.TaskDescription = $scope.TaskDescription;
            $scope.task.DescriptionId = result.data;
        });

        $scope.descEditMode = false;
    };

    $scope.EditTaskDescription = function () {
        $scope.descEditMode = true;
    };

    $scope.ModelChanged = function (user) {
        // commom method for any update operation
        console.log($scope.task);
        //post entire task to save
        DataService.updateTask($scope.task).then(function (result) {
            console.log(result);
        });
    };

    //$scope.$watch("task", function (newVal, oldVal) {
    //    if (newVal != oldVal) {
    //        $scope.msg = 'task is changed, new model:';
    //        console.log($scope.msg);
    //        console.log(newVal);
    //    }
    //}, true);


    $scope.AddPerson = function () {

        if (containsObject($scope.person.selected, $scope.task.AssignedUserList)) {
            console.log('Already added');
        } else {
            var assignedPerson = $scope.person.selected;
            assignedPerson.TaskId = $scope.task.Id;

            DataService.addTaskAssignment(assignedPerson).then(function (result) {
                console.log(result);
                if (result.status == 200) {
                    $scope.task.AssignedUserList.push(result.data);
                    $scope.person.selected = null;
                    $scope.addPersonButton = false;

                }
            });

            //$scope.task.AssignedUserList.push($scope.person.selected);
            //$scope.person.selected = null;
            //$scope.addPersonButton = false;
        }

    };

    $scope.RemovePerson = function (index) {
        $scope.task.AssignedUserList.splice(index, 1);
        console.log($scope.task.AssignedUserList);
    };

    // editor attachment
    var createStorageKey, host, uploadAttachment;

    $scope.trixAttachmentAdd = function (e) {
        var attachment;
        attachment = e.attachment;
        if (attachment.file) {
            return uploadAttachment(attachment);
        }
    }

    host = "https://d13txem1unpe48.cloudfront.net/";

    uploadAttachment = function (attachment) {
        console.log(attachment)
        var file, form, key, xhr;
        file = attachment.file;
        key = createStorageKey(file);
        form = new FormData;
        form.append("key", key);
        form.append("Content-Type", file.type);
        form.append("file", file);
        xhr = new XMLHttpRequest;
        xhr.open("POST", host, true);
        xhr.upload.onprogress = function (event) {
            var progress;
            progress = event.loaded / event.total * 100;
            return attachment.setUploadProgress(progress);
        };
        xhr.onload = function () {
            var href, url;
            if (xhr.status === 204) {
                url = href = host + key;
                console.log(url)
                return attachment.setAttributes({
                    url: url,
                    href: href
                });
            }
        };
        return xhr.send(form);
    };

    createStorageKey = function (file) {
        var date, day, time;
        date = new Date();
        day = date.toISOString().slice(0, 10);
        time = date.getTime();
        return "tmp/" + day + "/" + time + "-" + file.name;
    };


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
            return $http.post(apiUrl + '/SaveDescription?id=' + id + '&desc=' + escape(desc));
        };

        this.getTaskUsers = function () {
            return $http.post(apiUrl + '/UserList');
        };

        this.updateTask = function (task) {
            return $http.post(apiUrl + '/Edit', task);
        };

        this.addTaskAssignment = function (task) {
            return $http.post(apiUrl + '/AddTaskAssignment', task);
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