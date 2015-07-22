(function ($) {
    $.fn.trafficlight = function (options) {
        /*var settings = $.extend({
        }, options);*/
        debugger;
        this.html('');
        var wrapper = $('<div/>', { 'class': 'trafficlight-wrapper' });
        var data = options.Series.data[0];
        for (var i = 0; i < options.PlotBands.length; i++) {
            var trafficLightItem = $('<div/>', { 'class': 'trafficlight-item' });
            var traffictLightCircle = $('<div/>', { 'class': 'trafficlight-circle', style: 'background-color:' + options.PlotBands[i].color });
            if ((options.PlotBands[i].from <= data) && (data <= options.PlotBands[i].to)) {
                traffictLightCircle.css('opacity', '1');
                trafficLightItem.append('<span class="trafficlight-label">' + options.PlotBands[i].label + '</span>');
            }
            trafficLightItem.append(traffictLightCircle);
            wrapper.append(trafficLightItem);
        }

        this.append(wrapper);
        return this;
    };
})(jQuery);

//(function ($) {
//    $.fn.trafficlight = function (options) {
//        console.log(options);
//        console.log(this);
//        debugger;
//        var settings = $.extend({
//        }, options);

//        var wrapper = $('div').addClass('trafficlight-wrapper');
//        //<div class="">

//        /*for (var i = 0; i <= settings.PlotBands.length; i++) {
//            var wrapperItem = $('div').addClass('trafficlight-item');
//            wrapper.append(wrapperItem);
//        }*/

//        this.innerHTML(wrapper);
//        return this;
//    };
//})(jQuery);