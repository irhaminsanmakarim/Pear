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
    Pear.Template = {};
    Pear.Template.Editor = {};

    var artifactDesigner = Pear.Artifact.Designer;

    //helper
    artifactDesigner._formatKpi = function (kpi) {
        //console.log(kpi);
        if (kpi.loading) return kpi.text;
        return '<div class="clearfix"><div class="col-sm-12">' + kpi.Name + '</div></div>';
    };
    artifactDesigner._formatKpiSelection = function (kpi) {
        return kpi.Name || kpi.text;
    };
    artifactDesigner._kpiAutoComplete = function (context) {
        context.find('.kpi-list').select2({
            ajax: {
                url: $('#hidden-fields-holder').data('kpi-url'),
                dataType: 'json',
                delay: 250,
                data: function (params) {
                    return {
                        term: params.term, // search term
                        measurementId: $('#MeasurementId').val()
                    };
                },
                processResults: function (data, page) {
                    return data;
                },
                cache: true
            },
            escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
            minimumInputLength: 1,
            templateResult: Pear.Artifact.Designer._formatKpi, // omitted for brevity, see the source of this page
            templateSelection: Pear.Artifact.Designer._formatKpiSelection // omitted for brevity, see the source of this page
        });
    };
    artifactDesigner._colorPicker = function (context) {
        context.find('.colorpicker input').colpick({
            submit: 0,
            onChange: function (hsb, hex, rgb, el, bySetColor) {
                $(el).closest('.colorpicker').find('i').css('background-color', '#' + hex);
                if (!bySetColor) $(el).val('#' + hex);
            }
        }).keyup(function () {
            $(this).colpickSetColor(this.value.replace('#', ''));
        });
    };

    artifactDesigner.ListSetup = function () {
        $(document).on('click', '.artifact-view', function (e) {
            e.preventDefault();
            var $this = $(this);
            var callback = Pear.Artifact.Designer._previewCallbacks;
            $.ajax({
                url: $this.attr('href'),
                method: 'GET',
                success: function (data) {
                    if (callback.hasOwnProperty(data.GraphicType)) {
                        callback[data.GraphicType](data, $('#container'));
                    }
                    $('#graphic-preview').modal('show');
                }
            });
            $('#graphic-preview').on('show.bs.modal', function () {
                $('#container').css('visibility', 'hidden');
            });
            $('#graphic-preview').on('shown.bs.modal', function () {
                $('#container').css('visibility', 'initial');
                $('#container').highcharts().reflow();
            });
        });
    };
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
                    $('.graphic-properties').each(function (i, val) {
                        $(val).html('');
                    });
                    $('#graphic-settings').prev('.form-group').css('display', 'block');
                    if (callback.hasOwnProperty(type)) {
                        callback[type]();
                    }
                }
            });
        };
        var rangeDatePicker = function () {
            $('.datepicker').datetimepicker({
                format: "MM/DD/YYYY hh:00 A"
            });
            $('.datepicker').change(function (e) {
                console.log(this);
            });
            $('#PeriodeType').change(function (e) {
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
            $('#RangeFilter').change(function (e) {
                e.preventDefault();
                var $this = $(this);
                $('#range-holder').prop('class', $this.val().toLowerCase().trim());
            });
            var original = $('#RangeFilter').clone(true);
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
                $('#RangeFilter').replaceWith(originalClone);
            };

            rangeFilterSetup($('#PeriodeType').val().toLowerCase().trim());
            $('#PeriodeType').change(function (e) {
                e.preventDefault();
                var $this = $(this);
                rangeFilterSetup($this.val().toLowerCase().trim());
                $('#range-holder').removeAttr('class');
            });

        };

        $('#graphic-type').change(function (e) {
            e.preventDefault();
            var $this = $(this);
            loadGraph($this.data('graph-url'), $this.val());
        });

        var initialGraphicType = $('#graphic-type');
        loadGraph(initialGraphicType.data('graph-url'), initialGraphicType.val());
        rangeControl();
        rangeDatePicker();
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
                        callback[data.GraphicType](data, $('#container'));
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
            $('.series-template .remove').click(function (e) {
                e.preventDefault();
                var $this = $(this);
                $this.closest('.series-template').remove();
            });
            $('.stack-template .remove').click(function (e) {
                e.preventDefault();
                var $this = $(this);
                $this.closest('.stack-template').remove();
            });
        }
        var addSeries = function () {
            var seriesCount = 0;
            $('#add-series').click(function (e) {
                e.preventDefault();
                var seriesTemplate = $('.series-template.original').clone(true);
                Pear.Artifact.Designer._kpiAutoComplete(seriesTemplate);
                Pear.Artifact.Designer._colorPicker(seriesTemplate);
                $('<input>').attr({
                    type: 'hidden',
                    id: 'foo',
                    name: 'BarChart.Series.Index',
                    value: seriesCount
                }).appendTo(seriesTemplate);
                seriesTemplate.removeClass('original');
                seriesTemplate.attr('data-series-pos', seriesCount);
                if (seriesCount !== 0) {
                    var fields = ['Label', 'KpiId', 'ValueAxis', 'Color'];
                    for (var i in fields) {
                        var field = fields[i];
                        seriesTemplate.find('#BarChart_Series_0__' + field).attr('name', 'BarChart.Series[' + seriesCount + '].' + field);
                    }
                }
                seriesTemplate.addClass($('#seriesType').val().toLowerCase());
                seriesTemplate.addClass($('#bar-value-axis').val());
                $('#series-holder').append(seriesTemplate);
                seriesCount++;
            });
        };
        var addStack = function () {
            var stackCount = 0;
            $('.add-stack').click(function (e) {
                e.preventDefault();
                var $this = $(this);
                var stackTemplate = $('.stack-template.original').clone(true);
                Pear.Artifact.Designer._kpiAutoComplete(stackTemplate);
                Pear.Artifact.Designer._colorPicker(stackTemplate);
                stackTemplate.removeClass('original');
                var seriesPos = $this.closest('.series-template').data('series-pos');
                $('<input>').attr({
                    type: 'hidden',
                    id: 'foo',
                    name: 'BarChart.Series['+seriesPos+'].Stacks.Index',
                    value: stackCount
                }).appendTo(stackTemplate);
                var fields = ['Label', 'KpiId', 'ValueAxis', 'Color'];
                for (var i in fields) {
                    var field = fields[i];
                    stackTemplate.find('#BarChart_Series_0__Stacks_0__' + field).attr('name', 'BarChart.Series[' + seriesPos + '].Stacks[' + stackCount + '].' + field);
                }
                $this.closest('.stacks-holder').append(stackTemplate);
                stackCount++;
            });
        };

        removeSeriesOrStack();
        addSeries();
        addStack();
    };
    artifactDesigner._previewCallbacks.bar = function (data, container) {
        if (data.BarChart.SeriesType == "single-stack") {
            Pear.Artifact.Designer._displayBasicBarChart(data, container);
        } else if (data.BarChart.SeriesType == "multi-stack") {
            Pear.Artifact.Designer._displayMultistacksBarChart(data, container);
        } else {
            Pear.Artifact.Designer._displayMultistacksGroupedBarChart(data, container);
        }
    };
    artifactDesigner._displayBasicBarChart = function (data, container) {
        container.highcharts({
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
    artifactDesigner._displayMultistacksBarChart = function (data, container) {
        container.highcharts({
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
            //legend: {
            //    align: 'right',
            //    x: -30,
            //    verticalAlign: 'top',
            //    y: 25,
            //    floating: true,
            //    backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
            //    borderColor: '#CCC',
            //    borderWidth: 1,
            //    shadow: false
            //},
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
    artifactDesigner._displayMultistacksGroupedBarChart = function (data, container) {
        container.highcharts({
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
    artifactDesigner._setupCallbacks.baraccumulative = function () {
        Pear.Artifact.Designer._setupCallbacks.bar();
    };
    artifactDesigner._previewCallbacks.baraccumulative = function (data, container) {
        Pear.Artifact.Designer._previewCallbacks.bar(data, container);
    };
    artifactDesigner._setupCallbacks.barachievement = function () {
        $('#bar-value-axis').val('KpiActual');
        $('#graphic-settings').prev('.form-group').css('display', 'none');
        Pear.Artifact.Designer._setupCallbacks.bar();
    };
    artifactDesigner._previewCallbacks.barachievement = function (data, container) {
        Pear.Artifact.Designer._previewCallbacks.bar(data, container);
    };
    //line chart
    artifactDesigner._setupCallbacks.line = function () {
        var removeSeriesOrStack = function () {
            $('.series-template .remove').click(function (e) {
                e.preventDefault();
                var $this = $(this);
                $this.closest('fieldset').remove();
            });
        }
        var addSeries = function () {
            console.log('add-series');
            var seriesCount = 0;
            $('#add-series').click(function (e) {
                console.log('series-click');
                e.preventDefault();
                var seriesTemplate = $('.series-template.original').clone(true);

                Pear.Artifact.Designer._kpiAutoComplete(seriesTemplate);
                Pear.Artifact.Designer._colorPicker(seriesTemplate);
                $('<input>').attr({
                    type: 'hidden',
                    id: 'foo',
                    name: 'LineChart.Series.Index',
                    value: seriesCount
                }).appendTo(seriesTemplate);
                seriesTemplate.removeClass('original');
                seriesTemplate.attr('data-series-pos', seriesCount);
                if (seriesCount !== 0) {
                    var fields = ['Label', 'KpiId', 'Color'];
                    for (var i in fields) {
                        var field = fields[i];
                        seriesTemplate.find('#LineChart_Series_0__' + field).attr('name', 'LineChart.Series[' + seriesCount + '].' + field);
                    }
                }
                $('#series-holder').append(seriesTemplate);
                seriesCount++;
            });
        };
        removeSeriesOrStack();
        addSeries();
    }
    artifactDesigner._previewCallbacks.line = function (data, container) {
        container.highcharts({
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

    //speedometer
    artifactDesigner._setupCallbacks.speedometer = function () {
        var removePlot = function () {
            $('.plot-band-template .remove').click(function (e) {
                e.preventDefault();
                var $this = $(this);
                $this.closest('.plot-band-template').remove();
            });
        };

        var addPlot = function () {
            var plotPos = 0;
            $('#add-plot').click(function (e) {
                e.preventDefault();
                var $this = $(this);
                var plotBandTemplate = $('.plot-band-template.original').clone(true);
                plotBandTemplate.removeClass('original');
                Pear.Artifact.Designer._kpiAutoComplete(plotBandTemplate);
                $('<input>').attr({
                    type: 'hidden',
                    id: 'foo',
                    name: 'SpeedometerChart.PlotBands.Index',
                    value: plotPos
                }).appendTo(plotBandTemplate);
                if (plotPos !== 0) {
                    var fields = ['From', 'To', 'Color'];
                    for (var i in fields) {
                        var field = fields[i];
                        plotBandTemplate.find('#SpeedometerChart_PlotBands_0__' + field).attr('name', 'SpeedometerChart.PlotBands[' + plotPos + '].' + field).attr('id', 'plot-bands-' + i);
                    }
                }
                Pear.Artifact.Designer._colorPicker(plotBandTemplate);
                $('#plot-bands-holder').append(plotBandTemplate);
                plotPos++;
            });
        };

        

        var rangeDatePicker = function () {
            $('.datepicker').datetimepicker({
                format: "MM/DD/YYYY hh:00 A"
            });
            $('.datepicker').change(function (e) {
                console.log(this);
            });
            $('#SpeedometerChart_PeriodeType').change(function (e) {
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
            $('#SpeedometerChart_RangeFilter').change(function (e) {
                e.preventDefault();
                var $this = $(this);
                $('#range-holder').prop('class', $this.val().toLowerCase().trim());
            });
            var original = $('#SpeedometerChart_RangeFilter').clone(true);
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
                $('#SpeedometerChart_RangeFilter').replaceWith(originalClone);
            };

            rangeFilterSetup($('#SpeedometerChart_PeriodeType').val().toLowerCase().trim());
            $('#SpeedometerChart_PeriodeType').change(function (e) {
                e.preventDefault();
                var $this = $(this);
                rangeFilterSetup($this.val().toLowerCase().trim());
                $('#range-holder').removeAttr('class');
            });

        };

        Pear.Artifact.Designer._kpiAutoComplete($('#graphic-settings'));
        removePlot();
        addPlot();
        //rangeControl();
        //rangeDatePicker();
    };
    artifactDesigner._previewCallbacks.speedometer = function (data, container) {
        container.highcharts({
            chart: {
                type: 'gauge',
                plotBackgroundColor: null,
                plotBackgroundImage: null,
                plotBorderWidth: 0,
                plotShadow: false
            },

            title: {
                text: data.SpeedometerChart.Title
            },

            pane: {
                startAngle: -150,
                endAngle: 150,
                background: [{
                    backgroundColor: {
                        linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                        stops: [
                            [0, '#FFF'],
                            [1, '#333']
                        ]
                    },
                    borderWidth: 0,
                    outerRadius: '109%'
                }, {
                    backgroundColor: {
                        linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                        stops: [
                            [0, '#333'],
                            [1, '#FFF']
                        ]
                    },
                    borderWidth: 1,
                    outerRadius: '107%'
                }, {
                    // default background
                }, {
                    backgroundColor: '#DDD',
                    borderWidth: 0,
                    outerRadius: '105%',
                    innerRadius: '103%'
                }]
            },

            // the value axis
            yAxis: {
                min: 0,
                max: 200,

                minorTickInterval: 'auto',
                minorTickWidth: 1,
                minorTickLength: 10,
                minorTickPosition: 'inside',
                minorTickColor: '#666',

                tickPixelInterval: 30,
                tickWidth: 2,
                tickPosition: 'inside',
                tickLength: 10,
                tickColor: '#666',
                labels: {
                    step: 2,
                    rotation: 'auto'
                },
                title: {
                    text: data.SpeedometerChart.ValueAxisTitle
                },
                plotBands: data.SpeedometerChart.PlotBands
            },

            series: [{
                name: data.SpeedometerChart.Series.name,
                data: data.SpeedometerChart.Series.data,
                tooltip: {
                    valueSuffix: ' ' + data.SpeedometerChart.ValueAxisTitle
                }
            }]

        });
    }

    var templateEditor = Pear.Template.Editor;

    templateEditor._artifactSelectField = function (context) {
        context.find('.artifact-list').select2({
            ajax: {
                url: $('#hidden-fields-holder').data('artifact-url'),
                dataType: 'json',
                delay: 250,
                data: function (params) {
                    return {
                        term: params.term
                    };
                },
                processResults: function (data, page) {
                    return data;
                },
                cache: true
            },
            escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
            minimumInputLength: 1,
            templateResult: Pear.Artifact.Designer._formatKpi, // omitted for brevity, see the source of this page
            templateSelection: Pear.Artifact.Designer._formatKpiSelection // omitted for brevity, see the source of this page
        });
    };
    templateEditor.LayoutSetup = function () {
        $('.column-width').change(function () {
            var colWidth = $(this).val();
            var $column = $(this).closest('.layout-column');
            var $row = $(this).closest('.layout-row');
            var $currentCols =  $row.children('.layout-column');
            $column.css('width', colWidth + '%');
            var colIndex = $row.find('.layout-column').index($column);
            var remainWidth = 100;
            var remainLength = $currentCols.length - colIndex - 1;
            $currentCols.each(function (i, val) {
                if (i <= colIndex) {
                    remainWidth -= parseFloat($(val)[0].style.width.replace('%',''));
                }else{
                    $(val).css('width', (remainWidth/remainLength) + '%');
                }
            });
        });
        var addColumn = function () {
            var columnCount = 2;
            $('.add-column').click(function () {
                var $this = $(this);
                var $row = $(this).parent().find('.layout-row');
                var currentCols = $row.children('.layout-column').length;
                var newWidth = 100 / (currentCols + 1);
                $row.children('.layout-column').each(function (i, val) {
                    $(val).css('width', newWidth + '%');
                });
                var newColumn = $('.layout-column.original').clone(true);
                newColumn.removeClass('original');
                newColumn.css('width', newWidth + '%');
                Pear.Template.Editor._artifactSelectField(newColumn);
                $('<input>').attr({
                    type: 'hidden',
                    id: 'foo',
                    name: 'LayoutRows[' + $row.data('row-pos') + '].LayoutColumns.Index',
                    value: columnCount
                }).prependTo(newColumn);
                newColumn.find('.column-width').attr('name', 'LayoutRows[' + $row.data('row-pos') + '].LayoutColumns[' + columnCount + '].Width');
                newColumn.find('.artifact-list').attr('name', 'LayoutRows[' + $row.data('row-pos') + '].LayoutColumns[' + columnCount + '].ArtifactId');
                $row.append(newColumn);
                columnCount++;
            });
        };
       
        var addRow = function () {
            var rowCount = 1;
            $('.add-row').click(function () {
                var row = $('.layout-row-wrapper.original').clone(true);
                row.removeClass('original');
                row.find('.layout-column.original').removeClass('original');
                row.find('.layout-row').attr('data-row-pos', rowCount);
                Pear.Template.Editor._artifactSelectField(row);
                $('<input>').attr({
                    type: 'hidden',
                    id: 'foo',
                    name: 'LayoutRows.Index',
                    value: rowCount
                }).prependTo(row.find('.layout-row'));

                $('<input>').attr({
                    type: 'hidden',
                    id: 'foo',
                    name: 'LayoutRows['+rowCount+'].LayoutColumns.Index',
                    value: 1
                }).prependTo(row.find('.layout-column'));

                    row.find('.column-width').attr('name', 'LayoutRows[' + rowCount + '].LayoutColumns[1].Width');
                    row.find('.artifact-list').attr('name', 'LayoutRows[' + rowCount + '].LayoutColumns[1].ArtifactId');
                $('#rows-holder').append(row);
                rowCount++;
            });

        };
        $('.remove-row').click(function () {
            $(this).closest('.layout-row-wrapper').remove();
        });
        $('.remove-column').click(function () {
            var $this = $(this);
            var $column = $(this).closest('.layout-column');
            var $row = $(this).closest('.layout-row');
            var currentCols = $row.children('.layout-column').length;
            var newWidth = 100 / (currentCols - 1);
            $column.remove();
            $row.children('.layout-column').each(function (i, val) {
                $(val).css('width', newWidth + '%');
            });
        });
        addRow();
        addColumn();
    };
    templateEditor.ViewSetup = function () {
        $('.artifact-holder').each(function (i, val) {
            var $holder = $(val);
            var url = $holder.data('artifact-url');
            var callback = Pear.Artifact.Designer._previewCallbacks;
            $.ajax({
                url: url,
                method: 'GET',
                success: function (data) {
                    if (callback.hasOwnProperty(data.GraphicType)) {
                        callback[data.GraphicType](data, $holder);
                    }
                }
            });
        });
    };

    $(document).ready(function () {
        if ($('.artifact-designer').length) {
            Pear.Artifact.Designer.GraphicSettingSetup();
            Pear.Artifact.Designer.Preview();
        }
        if ($('.artifact-list').length) {
            Pear.Artifact.Designer.ListSetup();
        }
        if ($('.template-editor').length) {
            Pear.Template.Editor.LayoutSetup();
        }
        if ($('.template-view').length) {
            Pear.Template.Editor.ViewSetup();
        }
    });
    window.Pear = Pear;
}(window, jQuery, undefined));