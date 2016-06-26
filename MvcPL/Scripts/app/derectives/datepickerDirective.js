bookShopApp.directive('datepicker', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            $(function () {
                element.datetimepicker({
                    onChangeDateTime: function (dp, $input) {
                        scope.DateOfDelivery = $input.val();
                    },
                    minDate: 0
                });
            });
        }
    }
});
