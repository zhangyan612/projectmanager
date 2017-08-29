(function () {
    console.log('loading mainJs without api');
    // add month scale
    gantt.config.scale_unit = "week";
    gantt.config.step = 1;
    gantt.templates.date_scale = function (date) {
        var dateToStr = gantt.date.date_to_str("%d %M");
        var endDate = gantt.date.add(gantt.date.add(date, 1, "week"), -1, "day");
        return dateToStr(date) + " - " + dateToStr(endDate);
    };
    gantt.config.subscales = [
        { unit: "day", step: 1, date: "%D" }
    ];
    gantt.config.scale_height = 50;

    // scale config
    function setScaleConfig(value) {
        switch (value) {
            case "1":
                gantt.config.scale_unit = "day";
                gantt.config.step = 1;
                gantt.config.date_scale = "%d %M";
                gantt.config.subscales = [];
                gantt.config.scale_height = 27;
                gantt.templates.date_scale = null;
                break;
            case "2":
                var weekScaleTemplate = function (date) {
                    var dateToStr = gantt.date.date_to_str("%d %M");
                    var endDate = gantt.date.add(gantt.date.add(date, 1, "week"), -1, "day");
                    return dateToStr(date) + " - " + dateToStr(endDate);
                };

                gantt.config.scale_unit = "week";
                gantt.config.step = 1;
                gantt.templates.date_scale = weekScaleTemplate;
                gantt.config.subscales = [
                    { unit: "day", step: 1, date: "%D" }
                ];
                gantt.config.scale_height = 50;
                break;
            case "3":
                gantt.config.scale_unit = "month";
                gantt.config.date_scale = "%F, %Y";
                gantt.config.subscales = [
                    { unit: "day", step: 1, date: "%j, %D" }
                ];
                gantt.config.scale_height = 50;
                gantt.templates.date_scale = null;
                break;
            case "4":
                gantt.config.scale_unit = "year";
                gantt.config.step = 1;
                gantt.config.date_scale = "%Y";
                gantt.config.min_column_width = 50;

                gantt.config.scale_height = 90;
                gantt.templates.date_scale = null;

                gantt.config.subscales = [
                    { unit: "month", step: 1, date: "%M" }
                ];
                break;
        }
    }

    //setScaleConfig('3');

    // reorder config
    gantt.config.order_branch = true;
    gantt.config.order_branch_free = true;

    //left column descriptions
    //gantt.config.columns = [
    //    { name: "text", label: "Task name", tree: true, width: '*' },
    //    { name: "end_date", label: "Due Date", align: "center" },
    //    {
    //        name: "assigned", label: "Assigned to", align: "center", width: 100,
    //        template: function (item) {
    //            if (!item.users) return "";
    //            return item.users.join(", ");
    //        }
    //    }
    //];

    // configure milestone description
    gantt.templates.rightside_text = function (start, end, task) {
        if (task.type == gantt.config.types.milestone) {
            return task.text;
        }
        return "";
    };
    // add task duration to left
    gantt.templates.leftside_text = function (start, end, task) {
        return task.duration + " days";
    };

    // completed buttons
    gantt.locale.labels["complete_button"] = "Complete";
    gantt.config.buttons_left = ["dhx_save_btn", "dhx_cancel_btn", "complete_button"];

    gantt.templates.task_class = function (start, end, task) {
        if (task.progress == 1)
            return "completed_task";
        return "";
    };


    // add section to type selection: task, project or milestone
    gantt.config.lightbox.sections = [
		{ name: "description", height: 70, map_to: "text", type: "textarea", focus: true },
		{ name: "type", type: "typeselect", map_to: "type" },
		{ name: "time", height: 72, type: "duration", map_to: "auto" }
    ];
           
    gantt.config.xml_date = "%Y-%m-%d %H:%i:%s"; // format of dates in XML

    //// init logic move to page
    //gantt.init("ganttContainer"); // initialize gantt
    //gantt.load("/Gantt/Data?ProjectId=50fd56d8-62c6-40fe-9125-bd27b64d74f6", "json");

    //// enable dataProcessor
    //var dp = new dataProcessor("/Gantt/Save?ProjectId=50fd56d8-62c6-40fe-9125-bd27b64d74f6");
    //dp.init(gantt);


    gantt.templates.leftside_text = function (start, end, task) {
        return task.duration + " days";
    };

    //completed events
    gantt.attachEvent("onLightboxButton", function (button_id, node, e) {
        if (button_id == "complete_button") {
            var id = gantt.getState().lightbox;
            gantt.getTask(id).progress = 1;
            gantt.updateTask(id)
            gantt.hideLightbox();
        }
    });

    gantt.attachEvent("onBeforeLightbox", function (id) {
        var task = gantt.getTask(id);
        if (task.progress == 1) {
            gantt.message({ text: "This task is already completed!", type: "completed" });
            //return false;
        }
        return true;
    });

    // Show progress button
    var showProgress = function () {
        gantt.templates.progress_text = function (start, end, task) {
            return "<span style='text-align:left;'>" + Math.round(task.progress * 100) + "% </span>";
        };
    }


    var func = function (e) {
        e = e || window.event;
        var el = e.target || e.srcElement;
        var value = el.value;
        setScaleConfig(value);
        console.log(value);
        // change how to render gantt progress text
        if (value != 4) {
            gantt.templates.progress_text = function (start, end, task) {
                return "<span style='text-align:left;'>" + Math.round(task.progress * 100) + "% </span>";
            };
        } else {
            gantt.templates.progress_text = function (start, end, task) {
                return "";
            };
        }
        gantt.render();
    };

    var els = document.getElementsByName("scale");
    for (var i = 0; i < els.length; i++) {
        els[i].onclick = func;
    }



    ////// init logic move to page
    //gantt.init("ganttContainer"); // initialize gantt
    //gantt.load("/Gantt/Data?ProjectId=50fd56d8-62c6-40fe-9125-bd27b64d74f6", "json");

    //// enable dataProcessor
    //var dp = new dataProcessor("/Gantt/Save?ProjectId=50fd56d8-62c6-40fe-9125-bd27b64d74f6");
    //dp.init(gantt);




})();