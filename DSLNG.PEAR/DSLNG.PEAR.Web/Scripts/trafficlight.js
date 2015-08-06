(function ($) {
    $.fn.trafficlight = function (options) {
        this.html('');
        var wrapper = $('<div/>', { 'class': 'trafficlight-wrapper' });
        var data = options.Series.data[0];
        
        var containerHeight = this.height();
        var circleHeight = containerHeight / (options.PlotBands.length + 3);
        var itemHeight = circleHeight + 40;
        var itemWidth = circleHeight + 20;
        
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
        
        this.append('<span style="font-size:16px;display: table;margin: 0 auto;">' + options.Title + '</span>');
        this.append('<span style="font-size:13px;display: table;margin: 0 auto;">' + options.Subtitle + '</span>');
        this.append(wrapper);
        return this;
    };
})(jQuery);

