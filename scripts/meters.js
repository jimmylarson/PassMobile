var app = angular.module("AngularfilterApp", []);
app.controller("filterctrl", function ($scope) {
    $scope.users = [{
        type: "W",
        name: "Test",
        freq: 3
    }, {
        type: "G",
        name: "Test",
        freq: 3
    }, {
        type: "E",
        name: "test",
        freq: 3
    }, {
        type: "G",
        name: "Test",
        freq: 3
    }];
});