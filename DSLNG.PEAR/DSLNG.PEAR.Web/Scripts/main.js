(function (window, $, undifined) {
    $(document).ready(function () {
        $('#graphic-type').change(function (e) {
            e.preventDefault();
            var $this = $(this);
            $.ajax({
                url: $this.data('graph-url'),
                data: 'type=' + $this.val(),
                cache: true,
                method: 'GET',
                success: function (data) {
                    $('#graphic-settings').html(data);
                    $('.datepicker:not(.yearly)').datepicker({
                        format: 'mm/dd/yyyy',
                    }).on('changeDate', function (ev) {
                        $(this).datepicker('hide');
                    });
                    $('.datepicker.yearly').datepicker({
                        format: " yyyy", // Notice the Extra space at the beginning
                        viewMode: "years",
                        minViewMode: "years"
                    }).on('changeDate', function (ev) {
                        $(this).datepicker('hide');
                    });
                    $('#PeriodeType').change(function (e) {
                        e.preventDefault();
                        console.log($(this).val());
                        if ($(this).val() == 'yearly') {
                            $('#yearly-interval').show();
                            $('#monthly-interval').hide();
                        } else {
                            $('#monthly-interval').show();
                            $('#yearly-interval').hide();
                        }
                    });
                    var seriesCount = 0;
                    $('#add-series').click(function (e) {
                        console.log('sini ah');
                        e.preventDefault();
                        var seriesTemplate = $('.series-template.original').clone(true);
                        seriesTemplate.find('.kpi-list').select2();
                        seriesTemplate.removeClass('original');
                        seriesTemplate.attr('data-series-pos', seriesCount);
                        if (seriesCount == 0) {
                            $('#series-wrapper').append(seriesTemplate);
                            seriesCount++;
                        } else {
                            var fields = ['Label', 'KpiId', 'ValueAxis', 'Aggregation'];
                            for (var i in fields) {
                                var field = fields[i];
                                seriesTemplate.find('#SeriesList_0__' + field).attr('name', 'SeriesList[' + seriesCount + '].' + field);
                            }
                            seriesCount++;
                        }
                        seriesTemplate.addClass($('#seriesType').val());
                        seriesTemplate.addClass($('#bar-value-axis').val());
                        $('#series-wrapper').append(seriesTemplate);
                    });

                    $('.add-stack').click(function (e) {
                        e.preventDefault();
                        var $this = $(this);
                        var stackTemplate = $('.stack-template.original').clone(true);
                        stackTemplate.removeClass('original');
                        stackTemplate.find('.kpi-list').select2();
                        //if ($this.closest('.stacks-wrapper').children().length == 1) {
                        //    $this.closest('.stacks-wrapper').append(stackTemplate);
                        //} else {
                        console.log('asu');
                        var stackPos = $this.closest('.stacks-wrapper').children().length;
                        var seriesPos = $this.closest('.series-template').data('series-pos');
                        var fields = ['Label', 'KpiId', 'ValueAxis', 'Aggregation'];
                        for (var i in fields) {
                            var field = fields[i];
                            console.log(stackTemplate.find('#SeriesList_0__Stacks_0__' + field));
                            stackTemplate.find('#SeriesList_0__Stacks_0__' + field).attr('name', 'SeriesList[' + seriesPos + '].Stacks[' + stackPos + '].' + field);
                        }
                        //}
                        $this.closest('.stacks-wrapper').append(stackTemplate);
                    });
                   
                }
            });
        });
        $('#graphic-type').change();
       
    });
}(window, jQuery, undefined));