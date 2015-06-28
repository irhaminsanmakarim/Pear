// Common
String.prototype.startsWith = function (str) {
    return this.substr(0, str.length) === str;
};
String.prototype.endsWith = function (str) {
    return this.indexOf(str, this.length - str.length) !== -1;
};
String.prototype.isNullOrEmpty = function () {
    return this == false || this === '';
};

(function (window, $, undifined) {
    var Pear = {};
    Pear.Artifact = {};
    Pear.Artifact.Designer = {};

    var artifactDesigner = Pear.Artifact.Designer;

    artifactDesigner.GraphicSettingSetup = function () {
        var callback = Pear.Artifact.Designer._setupCallbacks;
        var loadGraph = function (url, type) {
            $.ajax({
                url: url,
                data: 'type=' + type,
                cache: true,
                method: 'GET',
                success: function (data) {
                    $('#graphic-settings').html(data);
                    var $hiddenFields = $('#hidden-fields');
                    $('#hidden-fields-holder').html($hiddenFields.html());
                    $hiddenFields.remove();
                    if (callback.hasOwnProperty(type)) {
                        callback[type]();
                    }
                }
            });
        };

        $('#graphic-type').change(function (e) {
            e.preventDefault();
            var $this = $(this);
            loadGraph($this.data('graph-url'), $this.val());
        });

        var initialGraphicType = $('#graphic-type');
        loadGraph(initialGraphicType.data('graph-url'), initialGraphicType.val());
    };
    artifactDesigner._setupCallbacks = {};
   
    artifactDesigner.Preview = function () {
        $('#graphic-preview-btn').click(function (e) {
            e.preventDefault();
            var $this = $(this);
            var callback = Pear.Artifact.Designer._previewCallbacks;
            $.ajax({
                url: $this.data('preview-url'),
                data: $this.closest('form').serialize(),
                method: 'POST',
                success: function (data) {
                    if (callback.hasOwnProperty(data.GraphicType)) {
                        callback[data.GraphicType](data);
                    }
                    $('#graphic-preview').modal('show');
                }
            });
        });
        $('#graphic-preview').on('show.bs.modal', function () {
            $('#container').css('visibility', 'hidden');
        });
        $('#graphic-preview').on('shown.bs.modal', function () {
            $('#container').css('visibility', 'initial');
            $('#container').highcharts().reflow();
        });
    };
    artifactDesigner._previewCallbacks = {};

    //bar chart
    artifactDesigner._setupCallbacks.bar = function () {
        var removeSeriesOrStack = function () {
            $('.series-template .remove, .stack-template .remove').click(function (e) {
                e.preventDefault();
                var $this = $(this);
                $this.closest('fieldset').remove();
            });
        }
        var addSeries = function () {
            var seriesCount = 0;
            $('#add-series').click(function (e) {
                e.preventDefault();
                var seriesTemplate = $('.series-template.original').clone(true);
                seriesTemplate.find('.kpi-list').select2();
                seriesTemplate.removeClass('original');
                seriesTemplate.attr('data-series-pos', seriesCount);
                if (seriesCount !== 0) {
                    var fields = ['Label', 'KpiId', 'ValueAxis', 'Aggregation'];
                    for (var i in fields) {
                        var field = fields[i];
                        seriesTemplate.find('#BarChart_SeriesList_0__' + field).attr('name', 'BarChart.SeriesList[' + seriesCount + '].' + field);
                    }
                }
                seriesTemplate.addClass($('#seriesType').val().toLowerCase());
                seriesTemplate.addClass($('#bar-value-axis').val());
                $('#series-holder').append(seriesTemplate);
                seriesCount++;
            });
        };
        var addStack = function () {
            $('.add-stack').click(function (e) {
                e.preventDefault();
                var $this = $(this);
                var stackTemplate = $('.stack-template.original').clone(true);
                stackTemplate.removeClass('original');
                stackTemplate.find('.kpi-list').select2();
                var stackPos = $this.closest('.stacks-holder').children('fieldset').length;
                var seriesPos = $this.closest('.series-template').data('series-pos');
                var fields = ['Label', 'KpiId', 'ValueAxis', 'Aggregation'];
                for (var i in fields) {
                    var field = fields[i];
                    stackTemplate.find('#BarChart_SeriesList_0__Stacks_0__' + field).attr('name', 'BarChart.SeriesList[' + seriesPos + '].Stacks[' + stackPos + '].' + field);
                }
                $this.closest('.stacks-holder').append(stackTemplate);
            });
        };
        var rangeDatePicker = function () {
            $('.datepicker').datetimepicker({
                format: "MM/DD/YYYY hh:00 A"
            });
            $('.datepicker').change(function (e) {
                console.log(this);
            });
            $('#BarChart_PeriodeType').change(function (e) {
                e.preventDefault();
                var $this = $(this);
                var clearValue = $('.datepicker').each(function (i, val) {
                    $(val).val('');
                    $(val).data("DateTimePicker").destroy();
                });
                switch ($this.val().toLowerCase().trim()) {
                    case 'hourly':
                        $('.datepicker').datetimepicker({
                            format: "MM/DD/YYYY hh:00 A"
                        });
                        break;
                    case 'daily':
                        $('.datepicker').datetimepicker({
                            format: "MM/DD/YYYY"
                        });
                        break;
                    case 'weekly':
                        $('.datepicker').datetimepicker({
                            format: "MM/DD/YYYY",
                            daysOfWeekDisabled: [0, 2, 3, 4, 5, 6]
                        });
                        break;
                    case 'monthly':
                        $('.datepicker').datetimepicker({
                            format: "MM/YYYY"
                        });
                        break;
                    case 'yearly':
                        $('.datepicker').datetimepicker({
                            format: "YYYY"
                        });
                        break;
                    default:

                }
            });
        };
        var rangeControl = function () {
            $('#BarChart_RangeFilter').change(function (e) {
                e.preventDefault();
                var $this = $(this);
                $('#range-holder').prop('class', $this.val().toLowerCase().trim());
            });
            var original = $('#BarChart_RangeFilter').clone(true);
            var rangeFilterSetup = function (periodeType) {
                var toRemove = {};
                toRemove.hourly = ['CurrentWeek', 'CurrentMonth', 'CurrentYear', 'YTD', 'MTD'];
                toRemove.daily = ['CurrentHour', 'CurrentYear', 'DTD', 'YTD'];
                toRemove.weekly = ['CurrentHour', 'CurrentDay', 'DTD', 'YTD'];
                toRemove.monthly = ['CurrentHour', 'CurrentDay', 'CurrentWeek', 'DTD', 'MTD'];
                toRemove.yearly = ['CurrentHour', 'CurrentDay', 'CurrentWeek', 'CurrentMonth', 'DTD', 'MTD'];
                var originalClone = original.clone(true);
                originalClone.find('option').each(function (i, val) {
                    if (toRemove[periodeType].indexOf(originalClone.find(val).val()) > -1) {
                        originalClone.find(val).remove();
                    }
                });
                $('#BarChart_RangeFilter').replaceWith(originalClone);
            };

            rangeFilterSetup($('#BarChart_PeriodeType').val().toLowerCase().trim());
            $('#BarChart_PeriodeType').change(function (e) {
                e.preventDefault();
                var $this = $(this);
                rangeFilterSetup($this.val().toLowerCase().trim());
                $('#range-holder').removeAttr('class');
            });

        };
        rangeControl();
        rangeDatePicker();
        removeSeriesOrStack();
        addSeries();
        addStack();
    };
    artifactDesigner._previewCallbacks.bar = function (data) {
        if (data.BarChart.SeriesType == "single-stack") {
            Pear.Artifact.Designer._displayBasicBarChart(data);
        } else if (data.BarChart.SeriesType == "multi-stack") {
            Pear.Artifact.Designer._displayMultistacksBarChart(data);
        } else {
            Pear.Artifact.Designer._displayMultistacksGroupedBarChart(data);
        }
    };
    artifactDesigner._displayBasicBarChart = function (data) {
        $('#container').highcharts({
            chart: {
                type: 'column'
            },
            title: {
                text: data.BarChart.Title
            },
            xAxis: {
                categories: data.BarChart.Periodes,
                crosshair: true
            },
            yAxis: {
                min: 0,
                title: {
                    text: data.BarChart.ValueAxisTitle
                }
            },
            tooltip: {
                headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b>{point.y:.1f} ' + data.BarChart.ValueAxisTitle + '</b></td></tr>',
                footerFormat: '</table>',
                shared: true,
                useHTML: true
            },
            plotOptions: {
                column: {
                    pointPadding: 0.2,
                    borderWidth: 0
                }
            },
            series: data.BarChart.Series
        });
    };
    artifactDesigner._displayMultistacksBarChart = function (data) {
        $('#container').highcharts({
            chart: {
                type: 'column'
            },
            title: {
                text: data.BarChart.Title
            },
            xAxis: {
                categories: data.BarChart.Periodes,
                crosshair: true
            },
            yAxis: {
                min: 0,
                title: {
                    text: data.BarChart.ValueAxisTitle
                },
                stackLabels: {
                    enabled: true,
                    style: {
                        fontWeight: 'bold',
                        color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                    }
                }
            },
            legend: {
                align: 'right',
                x: -30,
                verticalAlign: 'top',
                y: 25,
                floating: true,
                backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
                borderColor: '#CCC',
                borderWidth: 1,
                shadow: false
            },
            tooltip: {
                formatter: function () {
                    return '<b>' + this.x + '</b><br/>' +
                        this.series.name + ': ' + this.y + '<br/>' +
                        'Total: ' + this.point.stackTotal;
                }
            },
            plotOptions: {
                column: {
                    stacking: 'normal',
                    dataLabels: {
                        enabled: true,
                        color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white',
                        style: {
                            textShadow: '0 0 3px black'
                        }
                    }
                }
            },
            series: data.BarChart.Series
        });
    };
    artifactDesigner._displayMultistacksGroupedBarChart = function (data) {
        $('#container').highcharts({
            chart: {
                type: 'column'
            },
            title: {
                text: data.BarChart.Title
            },
            xAxis: {
                categories: data.BarChart.Periodes,
                crosshair: true
            },
            yAxis: {
                min: 0,
                title: {
                    text: data.BarChart.ValueAxisTitle
                }
            },

            tooltip: {
                formatter: function () {
                    return '<b>' + this.x + '</b><br/>' +
                        this.series.name + ': ' + this.y + '<br/>' +
                        'Total: ' + this.point.stackTotal;
                }
            },

            plotOptions: {
                column: {
                    stacking: 'normal'
                }
            },
            series: data.BarChart.Series
        });
    }

    //line chart
    artifactDesigner._setupCallbacks.line = function () {
        var removeSeriesOrStack = function () {
            $('.series-template .remove, .stack-template .remove').click(function (e) {
                e.preventDefault();
                var $this = $(this);
                $this.closest('fieldset').remove();
            });
        }
        var addSeries = function () {
            var seriesCount = 0;
            $('#add-series').click(function (e) {
                e.preventDefault();
                var seriesTemplate = $('.series-template.original').clone(true);
                seriesTemplate.find('.kpi-list').select2();
                seriesTemplate.removeClass('original');
                seriesTemplate.attr('data-series-pos', seriesCount);
                if (seriesCount !== 0) {
                    var fields = ['Label', 'KpiId'];
                    for (var i in fields) {
                        var field = fields[i];
                        seriesTemplate.find('#LineChart_SeriesList_0__' + field).attr('name', 'LineChart.SeriesList[' + seriesCount + '].' + field);
                    }
                }
                seriesTemplate.addClass('singlestack');
                $('#series-holder').append(seriesTemplate);
                seriesCount++;
            });
        };
       
        var rangeDatePicker = function () {
            $('.datepicker').datetimepicker({
                format: "MM/DD/YYYY hh:00 A"
            });
            $('.datepicker').change(function (e) {
                console.log(this);
            });
            $('#BarChart_PeriodeType').change(function (e) {
                e.preventDefault();
                var $this = $(this);
                var clearValue = $('.datepicker').each(function (i, val) {
                    $(val).val('');
                    $(val).data("DateTimePicker").destroy();
                });
                switch ($this.val().toLowerCase().trim()) {
                    case 'hourly':
                        $('.datepicker').datetimepicker({
                            format: "MM/DD/YYYY hh:00 A"
                        });
                        break;
                    case 'daily':
                        $('.datepicker').datetimepicker({
                            format: "MM/DD/YYYY"
                        });
                        break;
                    case 'weekly':
                        $('.datepicker').datetimepicker({
                            format: "MM/DD/YYYY",
                            daysOfWeekDisabled: [0, 2, 3, 4, 5, 6]
                        });
                        break;
                    case 'monthly':
                        $('.datepicker').datetimepicker({
                            format: "MM/YYYY"
                        });
                        break;
                    case 'yearly':
                        $('.datepicker').datetimepicker({
                            format: "YYYY"
                        });
                        break;
                    default:

                }
            });
        };
        var rangeControl = function () {
            $('#LineChart_RangeFilter').change(function (e) {
                e.preventDefault();
                var $this = $(this);
                $('#range-holder').prop('class', $this.val().toLowerCase().trim());
            });
            var original = $('#LineChart_RangeFilter').clone(true);
            var rangeFilterSetup = function (periodeType) {
                var toRemove = {};
                toRemove.hourly = ['CurrentWeek', 'CurrentMonth', 'CurrentYear', 'YTD', 'MTD'];
                toRemove.daily = ['CurrentHour', 'CurrentYear', 'DTD', 'YTD'];
                toRemove.weekly = ['CurrentHour', 'CurrentDay', 'DTD', 'YTD'];
                toRemove.monthly = ['CurrentHour', 'CurrentDay', 'CurrentWeek', 'DTD', 'MTD'];
                toRemove.yearly = ['CurrentHour', 'CurrentDay', 'CurrentWeek', 'CurrentMonth', 'DTD', 'MTD'];
                var originalClone = original.clone(true);
                originalClone.find('option').each(function (i, val) {
                    if (toRemove[periodeType].indexOf(originalClone.find(val).val()) > -1) {
                        originalClone.find(val).remove();
                    }
                });
                $('#LineChart_RangeFilter').replaceWith(originalClone);
            };

            rangeFilterSetup($('#LineChart_PeriodeType').val().toLowerCase().trim());
            $('#LineChart_PeriodeType').change(function (e) {
                e.preventDefault();
                var $this = $(this);
                rangeFilterSetup($this.val().toLowerCase().trim());
                $('#range-holder').removeAttr('class');
            });

        };
        rangeControl();
        rangeDatePicker();
        removeSeriesOrStack();
        addSeries();
    }
    artifactDesigner._previewCallbacks.line = function (data) {
        $('#container').highcharts({
            title: {
                text: data.LineChart.Title,
                x: -20 //center
            },
            //subtitle: {
            //    text: 'Source: WorldClimate.com',
            //    x: -20
            //},
            xAxis: {
                categories: data.LineChart.Periodes
            },
            yAxis: {
                title: {
                    text: data.LineChart.ValueAxisTitle
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            tooltip: {
                valueSuffix: data.LineChart.ValueAxisTitle
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'middle',
                borderWidth: 0
            },
            series: data.LineChart.Series
        });
    }

    $(document).ready(function () {
        Pear.Artifact.Designer.GraphicSettingSetup();
        Pear.Artifact.Designer.Preview();
    });
    window.Pear = Pear;
}(window, jQuery, undefined));