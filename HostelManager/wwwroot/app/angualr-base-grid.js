angular.module("ngBaseGrid", []).factory("BaseGrid", function ($http, $timeout) {
    var main = this;

    var editModel = function () {
        var self = this;
        self.modalTitle = "新增";
        self.saveSuccess = false;
        self.saveError = false;
        self.errorInfo = "";
        self.postUrl = "";
        self.newItem = function () {
            self.clearVail();
            self.modalTitle = "新增";
            self.postUrl = $("#builder_table").attr("data-url").replace("/List", "/Create");
            $("#myModal").modal("show");
            $("#builder_form input").val("");
            $("#builder_form textarea").val("");

        };
        self.editItem = function () {
            var array = $("#builder_table").bootstrapTable("getSelections");
            if (array.length < 1) {
                Notify('请选择要编辑的数据', 'top-right', '2000', 'info', 'fa-desktop', true)
              
                return;
            }

            self.clearVail();
            self.modalTitle = "编辑";
            self.postUrl = $("#builder_table").attr("data-url").replace("/List", "/Update");
            $("#myModal").modal("show");
            main.Active = array[0];

        };
        self.removeItem = function () {
            var array = $("#builder_table").bootstrapTable("getSelections");
            if (array.length < 1) {
                Notify('请选择要删除的记录', 'top-right', '2000', 'info', 'fa-desktop', true)                           
                return;
            }

            self.postUrl = $("#builder_table").attr("data-url").replace("/List", "/Remove");
            $.confirm({
                title: '删除',
                content: '确定删除该记录吗',
                type: 'danger',
                buttons: {
                    ok: {
                        text: "确定",
                        btnClass: 'btn-primary',
                        keys: ['enter'],
                        action: function () {
                            $http({
                                method: "POST",
                                url: self.postUrl,
                                data: angular.toJson(array),
                                headers: { 'Content-Type': "application/json;charset=UTF-8" }
                            }).then(function success(result) {

                                $("#builder_table").bootstrapTable("refresh", { silent: true });
                                $("#builder_table").bootstrapTable("uncheckAll");//清除所有选择元素refresh
                                Notify('删除数据成功', 'top-right', '2000', 'success', 'fa-desktop', true)
                               
                            }, function error(result) {
                                self.saveError = true;
                                self.errorInfo = "保存服务器出错！错误代码: " + result.status;

                            });
                        }
                    },
                    cancel: {
                        text: "取消",
                    }
                }
            });

        };
        self.export = function () {

        };
        self.saveChanges = function () {
            if ($("form").valid()) {
                $http({
                    method: "POST",
                    url: self.postUrl,
                    data: $("#builder_form").serialize(),
                    headers: {
                        'Content-Type': "application/x-www-form-urlencoded"
                    }
                }).then(function success(result) {
                    self.saveSuccess = true;
                    $timeout(function () {
                        $("#builder_table").bootstrapTable("refresh", { silent: true });
                        $('#myModal').modal("hide");
                    }, 1000);
                }, function error(result) {
                    self.saveError = true;
                    self.errorInfo = "保存服务器出错！错误代码: " + result.status;

                });
            }

        };
        self.clearVail = function () {
            self.saveSuccess = false;
            self.saveError = false;
            self.errorInfo = "";
            $("span[class='text-danger field-validation-error']").each(function (index, node) {
                $(node).html("");
            });

        }

    }
    main.Edit = new editModel();
    main.init = function () {
        $(function () {
            $('#builder_table').on('page-change.bs.table', function (number, size) {
                $("#builder_table").bootstrapTable("uncheckAll");//清除所有选择元素
            });

        })

    }
    main.Active = {};
    main.init();
    return main;
});