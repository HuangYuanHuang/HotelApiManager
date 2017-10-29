(function () {
    'use strict';
    angular
        .module('ngSelect',[])
    .directive("selectControl", function () {
        var directive = {
            link: link,
            restrict: 'EA',
            template: '<select><option ng-repeat="item in nodes" value="{{item.id}}">{{item.text}}</option></select>',
            scope: {
                requestUrl: "@",
                requestInit:"="
            },
            replace: true,
            controller: function ($scope, $http) {
              
                $http({
                    method: "POST",
                    url: $scope.requestUrl,
                }).then(function success(result) {
                    $scope.nodes = result.data;
                    if (result.data.length > 0) {
                        $scope.requestInit = result.data[0].id;
                    }
                   
                }, function error(result) {


                });

            }

        };
        return directive;

        function link(scope, element, attrs) {
        }


    })

})();