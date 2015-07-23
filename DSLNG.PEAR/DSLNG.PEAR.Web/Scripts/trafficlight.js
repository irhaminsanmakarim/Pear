(function ($) {
    $.fn.trafficlight = function (options) {
        this.html('');
        var wrapper = $('<div/>', { 'class': 'trafficlight-wrapper' });
        var data = options.Series.data[0];
        
        console.log(this);
        console.log($(this));
        var containerHeight = this.height();
        console.log(containerHeight);
        var circleHeight = containerHeight / (options.PlotBands.length + 3);
        var itemHeight = circleHeight + 40;
        var itemWidth = circleHeight + 20;
        console.log(circleHeight);
        console.log(itemHeight);
        console.log(itemWidth);
        
        for (var i = 0; i < options.PlotBands.length; i++) {
            var trafficLightItem = $('<div/>', { 'class': 'trafficlight-item', 'style': 'height:' + itemHeight + 'px;width:' + itemHeight + 'px' });
            var traffictLightCircle = $('<div/>', { 'class': 'trafficlight-circle', style: 'background-color:' + options.PlotBands[i].color + ';height:' + circleHeight + 'px;width:' + circleHeight + 'px' });
            if ((options.PlotBands[i].from <= data) && (data <= options.PlotBands[i].to)) {
                traffictLightCircle.css('opacity', '1');
                trafficLightItem.append('<span class="trafficlight-label">' + options.PlotBands[i].label + '</span>');
            }
            trafficLightItem.append(traffictLightCircle);
            wrapper.append(trafficLightItem);
        }
        
        
        /*$('.trafficlight-item').width(itemWidth);
        $('.trafficlight-circle').height(circleHeight);
        $('.trafficlight-circle').width(circleHeight);
        */
        this.append('<span style="font-size:16px;display: table;margin: 0 auto;">' + options.Title + '</span>');
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