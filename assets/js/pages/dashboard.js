"use strict";

// Class definition
var KTDashboard = function() {

    // Sparkline Chart helper function
    var _initSparklineChart = function(src, data, color, border) {
        if (src.length == 0) {
            return;
        }

        var config = {
            type: 'line',
            data: {
                labels: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October"],
                datasets: [{
                    label: "",
                    borderColor: color,
                    borderWidth: border,

                    pointHoverRadius: 4,
                    pointHoverBorderWidth: 12,
                    pointBackgroundColor: Chart.helpers.color('#000000').alpha(0).rgbString(),
                    pointBorderColor: Chart.helpers.color('#000000').alpha(0).rgbString(),
                    pointHoverBackgroundColor: KTApp.getStateColor('danger'),
                    pointHoverBorderColor: Chart.helpers.color('#000000').alpha(0.1).rgbString(),
                    fill: false,
                    data: data,
                }]
            },
            options: {
                title: {
                    display: false,
                },
                tooltips: {
                    enabled: false,
                    intersect: false,
                    mode: 'nearest',
                    xPadding: 10,
                    yPadding: 10,
                    caretPadding: 10
                },
                legend: {
                    display: false,
                    labels: {
                        usePointStyle: false
                    }
                },
                responsive: true,
                maintainAspectRatio: true,
                hover: {
                    mode: 'index'
                },
                scales: {
                    xAxes: [{
                        display: false,
                        gridLines: false,
                        scaleLabel: {
                            display: true,
                            labelString: 'Month'
                        }
                    }],
                    yAxes: [{
                        display: false,
                        gridLines: false,
                        scaleLabel: {
                            display: true,
                            labelString: 'Value'
                        },
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },

                elements: {
                    point: {
                        radius: 4,
                        borderWidth: 12
                    },
                },

                layout: {
                    padding: {
                        left: 0,
                        right: 10,
                        top: 5,
                        bottom: 0
                    }
                }
            }
        };

        return new Chart(src, config);
    }

    // Daily Sales chart.
    // Based on Chartjs plugin - http://www.chartjs.org/
    var dailySales = function() {
        var chartContainer = KTUtil.getByID('kt_chart_daily_sales');

        if (!chartContainer) {
            return;
        }

        var chartData = {
            labels: ["Label 1", "Label 2", "Label 3", "Label 4", "Label 5", "Label 6", "Label 7", "Label 8", "Label 9", "Label 10", "Label 11", "Label 12", "Label 13", "Label 14", "Label 15", "Label 16"],
            datasets: [{
                //label: 'Dataset 1',
                backgroundColor: KTApp.getStateColor('success'),
                data: [
                    15, 20, 25, 30, 25, 20, 15, 20, 25, 30, 25, 20, 15, 10, 15, 20
                ]
            }, {
                //label: 'Dataset 2',
                backgroundColor: '#f3f3fb',
                data: [
                    15, 20, 25, 30, 25, 20, 15, 20, 25, 30, 25, 20, 15, 10, 15, 20
                ]
            }]
        };

        var chart = new Chart(chartContainer, {
            type: 'bar',
            data: chartData,
            options: {
                title: {
                    display: false,
                },
                tooltips: {
                    intersect: false,
                    mode: 'nearest',
                    xPadding: 10,
                    yPadding: 10,
                    caretPadding: 10
                },
                legend: {
                    display: false
                },
                responsive: true,
                maintainAspectRatio: false,
                barRadius: 4,
                scales: {
                    xAxes: [{
                        display: false,
                        gridLines: false,
                        stacked: true
                    }],
                    yAxes: [{
                        display: false,
                        stacked: true,
                        gridLines: false
                    }]
                },
                layout: {
                    padding: {
                        left: 0,
                        right: 0,
                        top: 0,
                        bottom: 0
                    }
                }
            }
        });
    }

    // Profit Share Chart.
    // Based on Chartjs plugin - http://www.chartjs.org/
    var profitShare = function() {        
        if (!KTUtil.getByID('kt_chart_profit_share')) {
            return;
        }

        var randomScalingFactor = function() {
            return Math.round(Math.random() * 100);
        };

        var config = {
            type: 'doughnut',
            data: {
                datasets: [{
                    data: [
                        35, 30, 35
                    ],
                    backgroundColor: [
                        KTApp.getStateColor('success'),
                        KTApp.getStateColor('danger'),
                        KTApp.getStateColor('brand')
                    ]
                }],
                labels: [
                    'Angular',
                    'CSS',
                    'HTML'
                ]
            },
            options: {
                cutoutPercentage: 75,
                responsive: true,
                maintainAspectRatio: false,
                legend: {
                    display: false,
                    position: 'top',
                },
                title: {
                    display: false,
                    text: 'Technology'
                },
                animation: {
                    animateScale: true,
                    animateRotate: true
                },
                tooltips: {
                    enabled: true,
                    intersect: false,
                    mode: 'nearest',
                    bodySpacing: 5,
                    yPadding: 10,
                    xPadding: 10, 
                    caretPadding: 0,
                    displayColors: false,
                    backgroundColor: KTApp.getStateColor('brand'),
                    titleFontColor: '#ffffff', 
                    cornerRadius: 4,
                    footerSpacing: 0,
                    titleSpacing: 0
                }
            }
        };

        var ctx = KTUtil.getByID('kt_chart_profit_share').getContext('2d');
        var myDoughnut = new Chart(ctx, config);
    }

    // Sales Stats.
    // Based on Chartjs plugin - http://www.chartjs.org/
    var salesStats = function() {
        if (!KTUtil.getByID('kt_chart_sales_stats')) {
            return;
        }

        var config = {
            type: 'line',
            data: {
                labels: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December",
                    "January", "February", "March", "April"
                ],
                datasets: [{
                    label: "Sales Stats",
                    borderColor: KTApp.getStateColor('brand'),
                    borderWidth: 2,
                    //pointBackgroundColor: KTApp.getStateColor('brand'),
                    backgroundColor: KTApp.getStateColor('brand'),                    
                    pointBackgroundColor: Chart.helpers.color('#ffffff').alpha(0).rgbString(),
                    pointBorderColor: Chart.helpers.color('#ffffff').alpha(0).rgbString(),
                    pointHoverBackgroundColor: KTApp.getStateColor('danger'),
                    pointHoverBorderColor: Chart.helpers.color(KTApp.getStateColor('danger')).alpha(0.2).rgbString(),
                    data: [
                        10, 20, 16,
                        18, 12, 40,
                        35, 30, 33,
                        34, 45, 40,
                        60, 55, 70,
                        65, 75, 62
                    ]
                }]
            },
            options: {
                title: {
                    display: false,
                },
                tooltips: {
                    intersect: false,
                    mode: 'nearest',
                    xPadding: 10,
                    yPadding: 10,
                    caretPadding: 10
                },
                legend: {
                    display: false,
                    labels: {
                        usePointStyle: false
                    }
                },
                responsive: true,
                maintainAspectRatio: false,
                hover: {
                    mode: 'index'
                },
                scales: {
                    xAxes: [{
                        display: false,
                        gridLines: false,
                        scaleLabel: {
                            display: true,
                            labelString: 'Month'
                        }
                    }],
                    yAxes: [{
                        display: false,
                        gridLines: false,
                        scaleLabel: {
                            display: true,
                            labelString: 'Value'
                        }
                    }]
                },

                elements: {
                    point: {
                        radius: 3,
                        borderWidth: 0,

                        hoverRadius: 8,
                        hoverBorderWidth: 2
                    }
                }
            }
        };

        var chart = new Chart(KTUtil.getByID('kt_chart_sales_stats'), config);
    }

    // Sales By KTUtillication Stats.
    // Based on Chartjs plugin - http://www.chartjs.org/
    var salesByApps = function() {
        // Init chart instances
        _initSparklineChart($('#kt_chart_sales_by_apps_1_1'), [10, 20, -5, 8, -20, -2, -4, 15, 5, 8], KTApp.getStateColor('success'), 2);
        _initSparklineChart($('#kt_chart_sales_by_apps_1_2'), [2, 16, 0, 12, 22, 5, -10, 5, 15, 2], KTApp.getStateColor('danger'), 2);
        _initSparklineChart($('#kt_chart_sales_by_apps_1_3'), [15, 5, -10, 5, 16, 22, 6, -6, -12, 5], KTApp.getStateColor('success'), 2);
        _initSparklineChart($('#kt_chart_sales_by_apps_1_4'), [8, 18, -12, 12, 22, -2, -14, 16, 18, 2], KTApp.getStateColor('warning'), 2);

        _initSparklineChart($('#kt_chart_sales_by_apps_2_1'), [10, 20, -5, 8, -20, -2, -4, 15, 5, 8], KTApp.getStateColor('danger'), 2);
        _initSparklineChart($('#kt_chart_sales_by_apps_2_2'), [2, 16, 0, 12, 22, 5, -10, 5, 15, 2], KTApp.getStateColor('dark'), 2);
        _initSparklineChart($('#kt_chart_sales_by_apps_2_3'), [15, 5, -10, 5, 16, 22, 6, -6, -12, 5], KTApp.getStateColor('brand'), 2);
        _initSparklineChart($('#kt_chart_sales_by_apps_2_4'), [8, 18, -12, 12, 22, -2, -14, 16, 18, 2], KTApp.getStateColor('info'), 2);
    }

    // Latest Updates.
    // Based on Chartjs plugin - http://www.chartjs.org/
    var latestUpdates = function() {
        if ($('#kt_chart_latest_updates').length == 0) {
            return;
        }

        var ctx = document.getElementById("kt_chart_latest_updates").getContext("2d");

        var config = {
            type: 'line',
            data: {
                labels: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October"],
                datasets: [{
                    label: "Sales Stats",
                    backgroundColor: KTApp.getStateColor('danger'), // Put the gradient here as a fill color
                    borderColor: KTApp.getStateColor('danger'),
                    pointBackgroundColor: Chart.helpers.color('#000000').alpha(0).rgbString(),
                    pointBorderColor: Chart.helpers.color('#000000').alpha(0).rgbString(),
                    pointHoverBackgroundColor: KTApp.getStateColor('success'),
                    pointHoverBorderColor: Chart.helpers.color('#000000').alpha(0.1).rgbString(),

                    //fill: 'start',
                    data: [
                        10, 14, 12, 16, 9, 11, 13, 9, 13, 15
                    ]
                }]
            },
            options: {
                title: {
                    display: false,
                },
                tooltips: {
                    intersect: false,
                    mode: 'nearest',
                    xPadding: 10,
                    yPadding: 10,
                    caretPadding: 10
                },
                legend: {
                    display: false
                },
                responsive: true,
                maintainAspectRatio: false,
                hover: {
                    mode: 'index'
                },
                scales: {
                    xAxes: [{
                        display: false,
                        gridLines: false,
                        scaleLabel: {
                            display: true,
                            labelString: 'Month'
                        }
                    }],
                    yAxes: [{
                        display: false,
                        gridLines: false,
                        scaleLabel: {
                            display: true,
                            labelString: 'Value'
                        },
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                elements: {
                    line: {
                        tension: 0.0000001
                    },
                    point: {
                        radius: 4,
                        borderWidth: 12
                    }
                }
            }
        };

        var chart = new Chart(ctx, config);
    }

    // Trends Stats.
    // Based on Chartjs plugin - http://www.chartjs.org/
    var trendsStats = function() {
        if ($('#kt_chart_trends_stats').length == 0) {
            return;
        }

        var ctx = document.getElementById("kt_chart_trends_stats").getContext("2d");

        var gradient = ctx.createLinearGradient(0, 0, 0, 240);
        gradient.addColorStop(0, Chart.helpers.color('#00c5dc').alpha(0.7).rgbString());
        gradient.addColorStop(1, Chart.helpers.color('#f2feff').alpha(0).rgbString());

        var config = {
            type: 'line',
            data: {
                labels: [
                    "January", "February", "March", "April", "May", "June", "July", "August", "September", "October",
                    "January", "February", "March", "April", "May", "June", "July", "August", "September", "October",
                    "January", "February", "March", "April", "May", "June", "July", "August", "September", "October",
                    "January", "February", "March", "April"
                ],
                datasets: [{
                    label: "Sales Stats",
                    backgroundColor: gradient, // Put the gradient here as a fill color
                    borderColor: '#0dc8de',

                    pointBackgroundColor: Chart.helpers.color('#ffffff').alpha(0).rgbString(),
                    pointBorderColor: Chart.helpers.color('#ffffff').alpha(0).rgbString(),
                    pointHoverBackgroundColor: KTApp.getStateColor('danger'),
                    pointHoverBorderColor: Chart.helpers.color('#000000').alpha(0.2).rgbString(),

                    //fill: 'start',
                    data: [
                        20, 10, 18, 15, 26, 18, 15, 22, 16, 12,
                        12, 13, 10, 18, 14, 24, 16, 12, 19, 21,
                        16, 14, 21, 21, 13, 15, 22, 24, 21, 11,
                        14, 19, 21, 17
                    ]
                }]
            },
            options: {
                title: {
                    display: false,
                },
                tooltips: {
                    intersect: false,
                    mode: 'nearest',
                    xPadding: 10,
                    yPadding: 10,
                    caretPadding: 10
                },
                legend: {
                    display: false
                },
                responsive: true,
                maintainAspectRatio: false,
                hover: {
                    mode: 'index'
                },
                scales: {
                    xAxes: [{
                        display: false,
                        gridLines: false,
                        scaleLabel: {
                            display: true,
                            labelString: 'Month'
                        }
                    }],
                    yAxes: [{
                        display: false,
                        gridLines: false,
                        scaleLabel: {
                            display: true,
                            labelString: 'Value'
                        },
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                elements: {
                    line: {
                        tension: 0.19
                    },
                    point: {
                        radius: 4,
                        borderWidth: 12
                    }
                },
                layout: {
                    padding: {
                        left: 0,
                        right: 0,
                        top: 5,
                        bottom: 0
                    }
                }
            }
        };

        var chart = new Chart(ctx, config);
    }

    // Trends Stats 2.
    // Based on Chartjs plugin - http://www.chartjs.org/
    var trendsStats2 = function() {
        if ($('#kt_chart_trends_stats_2').length == 0) {
            return;
        }

        var ctx = document.getElementById("kt_chart_trends_stats_2").getContext("2d");

        var config = {
            type: 'line',
            data: {
                labels: [
                    "January", "February", "March", "April", "May", "June", "July", "August", "September", "October",
                    "January", "February", "March", "April", "May", "June", "July", "August", "September", "October",
                    "January", "February", "March", "April", "May", "June", "July", "August", "September", "October",
                    "January", "February", "March", "April"
                ],
                datasets: [{
                    label: "Sales Stats",
                    backgroundColor: '#d2f5f9', // Put the gradient here as a fill color
                    borderColor: KTApp.getStateColor('brand'),

                    pointBackgroundColor: Chart.helpers.color('#ffffff').alpha(0).rgbString(),
                    pointBorderColor: Chart.helpers.color('#ffffff').alpha(0).rgbString(),
                    pointHoverBackgroundColor: KTApp.getStateColor('danger'),
                    pointHoverBorderColor: Chart.helpers.color('#000000').alpha(0.2).rgbString(),

                    //fill: 'start',
                    data: [
                        20, 10, 18, 15, 32, 18, 15, 22, 8, 6,
                        12, 13, 10, 18, 14, 24, 16, 12, 19, 21,
                        16, 14, 24, 21, 13, 15, 27, 29, 21, 11,
                        14, 19, 21, 17
                    ]
                }]
            },
            options: {
                title: {
                    display: false,
                },
                tooltips: {
                    intersect: false,
                    mode: 'nearest',
                    xPadding: 10,
                    yPadding: 10,
                    caretPadding: 10
                },
                legend: {
                    display: false
                },
                responsive: true,
                maintainAspectRatio: false,
                hover: {
                    mode: 'index'
                },
                scales: {
                    xAxes: [{
                        display: false,
                        gridLines: false,
                        scaleLabel: {
                            display: true,
                            labelString: 'Month'
                        }
                    }],
                    yAxes: [{
                        display: false,
                        gridLines: false,
                        scaleLabel: {
                            display: true,
                            labelString: 'Value'
                        },
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                elements: {
                    line: {
                        tension: 0.19
                    },
                    point: {
                        radius: 4,
                        borderWidth: 12
                    }
                },
                layout: {
                    padding: {
                        left: 0,
                        right: 0,
                        top: 5,
                        bottom: 0
                    }
                }
            }
        };

        var chart = new Chart(ctx, config);
    }

    // Trends Stats.
    // Based on Chartjs plugin - http://www.chartjs.org/
    var latestTrendsMap = function() {
        if ($('#kt_chart_latest_trends_map').length == 0) {
            return;
        }

        try {
            var map = new GMaps({
                div: '#kt_chart_latest_trends_map',
                lat: -12.043333,
                lng: -77.028333
            });
        } catch (e) {
            console.log(e);
        }
    }

    // Revenue Change.
    // Based on Morris plugin - http://morrisjs.github.io/morris.js/
    var revenueChange = function() {
        if ($('#kt_chart_revenue_change').length == 0) {
            return;
        }

    }

    // Support Tickets Chart.
    // Based on Morris plugin - http://morrisjs.github.io/morris.js/
    var supportCases = function() {
        if ($('#kt_chart_support_tickets').length == 0) {
            return;
        }

        Morris.Donut({
            element: 'kt_chart_support_tickets',
            data: [{
                    label: "Margins",
                    value: 20
                },
                {
                    label: "Profit",
                    value: 70
                },
                {
                    label: "Lost",
                    value: 10
                }
            ],
            labelColor: '#a7a7c2',
            colors: [
                KTApp.getStateColor('success'),
                KTApp.getStateColor('brand'),
                KTApp.getStateColor('danger')
            ]
            //formatter: function (x) { return x + "%"}
        });
    }

    // Support Tickets Chart.
    // Based on Chartjs plugin - http://www.chartjs.org/
    var supportRequests = function() {
        var container = KTUtil.getByID('kt_chart_support_requests');

        if (!container) {
            return;
        }

        var randomScalingFactor = function() {
            return Math.round(Math.random() * 100);
        };

        var config = {
            type: 'doughnut',
            data: {
                datasets: [{
                    data: [
                        35, 30, 35
                    ],
                    backgroundColor: [
                        KTApp.getStateColor('success'),
                        KTApp.getStateColor('danger'),
                        KTApp.getStateColor('brand')
                    ]
                }],
                labels: [
                    'Angular',
                    'CSS',
                    'HTML'
                ]
            },
            options: {
                cutoutPercentage: 75,
                responsive: true,
                maintainAspectRatio: false,
                legend: {
                    display: false,
                    position: 'top',
                },
                title: {
                    display: false,
                    text: 'Technology'
                },
                animation: {
                    animateScale: true,
                    animateRotate: true
                },
                tooltips: {
                    enabled: true,
                    intersect: false,
                    mode: 'nearest',
                    bodySpacing: 5,
                    yPadding: 10,
                    xPadding: 10, 
                    caretPadding: 0,
                    displayColors: false,
                    backgroundColor: KTApp.getStateColor('brand'),
                    titleFontColor: '#ffffff', 
                    cornerRadius: 4,
                    footerSpacing: 0,
                    titleSpacing: 0
                }
            }
        };

        var ctx = container.getContext('2d');
        var myDoughnut = new Chart(ctx, config);
    }

    // Activities Charts.
    // Based on Chartjs plugin - http://www.chartjs.org/
    var activitiesChart = function() {
        if ($('#kt_chart_activities').length == 0) {
            return;
        }

        var ctx = document.getElementById("kt_chart_activities").getContext("2d");

        var gradient = ctx.createLinearGradient(0, 0, 0, 240);
        gradient.addColorStop(0, Chart.helpers.color('#e14c86').alpha(1).rgbString());
        gradient.addColorStop(1, Chart.helpers.color('#e14c86').alpha(0.3).rgbString());

        var config = {
            type: 'line',
            data: {
                labels: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October"],
                datasets: [{
                    label: "Sales Stats",
                    backgroundColor: Chart.helpers.color('#e14c86').alpha(1).rgbString(),  //gradient
                    borderColor: '#e13a58',

                    pointBackgroundColor: Chart.helpers.color('#000000').alpha(0).rgbString(),
                    pointBorderColor: Chart.helpers.color('#000000').alpha(0).rgbString(),
                    pointHoverBackgroundColor: KTApp.getStateColor('light'),
                    pointHoverBorderColor: Chart.helpers.color('#ffffff').alpha(0.1).rgbString(),

                    //fill: 'start',
                    data: [
                        10, 14, 12, 16, 9, 11, 13, 9, 13, 15
                    ]
                }]
            },
            options: {
                title: {
                    display: false,
                },
                tooltips: {
                    mode: 'nearest',
                    intersect: false,
                    position: 'nearest',
                    xPadding: 10,
                    yPadding: 10,
                    caretPadding: 10
                },
                legend: {
                    display: false
                },
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    xAxes: [{
                        display: false,
                        gridLines: false,
                        scaleLabel: {
                            display: true,
                            labelString: 'Month'
                        }
                    }],
                    yAxes: [{
                        display: false,
                        gridLines: false,
                        scaleLabel: {
                            display: true,
                            labelString: 'Value'
                        },
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                elements: {
                    line: {
                        tension: 0.0000001
                    },
                    point: {
                        radius: 4,
                        borderWidth: 12
                    }
                },
                layout: {
                    padding: {
                        left: 0,
                        right: 0,
                        top: 10,
                        bottom: 0
                    }
                }
            }
        };

        var chart = new Chart(ctx, config);
    }

    // Bandwidth Charts 1.
    // Based on Chartjs plugin - http://www.chartjs.org/
    var bandwidthChart1 = function() {
        if ($('#kt_chart_bandwidth1').length == 0) {
            return;
        }

        var ctx = document.getElementById("kt_chart_bandwidth1").getContext("2d");

        var gradient = ctx.createLinearGradient(0, 0, 0, 240);
        gradient.addColorStop(0, Chart.helpers.color('#d1f1ec').alpha(1).rgbString());
        gradient.addColorStop(1, Chart.helpers.color('#d1f1ec').alpha(0.3).rgbString());

        var config = {
            type: 'line',
            data: {
                labels: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October"],
                datasets: [{
                    label: "Bandwidth Stats",
                    backgroundColor: gradient,
                    borderColor: KTApp.getStateColor('success'),

                    pointBackgroundColor: Chart.helpers.color('#000000').alpha(0).rgbString(),
                    pointBorderColor: Chart.helpers.color('#000000').alpha(0).rgbString(),
                    pointHoverBackgroundColor: KTApp.getStateColor('danger'),
                    pointHoverBorderColor: Chart.helpers.color('#000000').alpha(0.1).rgbString(),

                    //fill: 'start',
                    data: [
                        10, 14, 12, 16, 9, 11, 13, 9, 13, 15
                    ]
                }]
            },
            options: {
                title: {
                    display: false,
                },
                tooltips: {
                    mode: 'nearest',
                    intersect: false,
                    position: 'nearest',
                    xPadding: 10,
                    yPadding: 10,
                    caretPadding: 10
                },
                legend: {
                    display: false
                },
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    xAxes: [{
                        display: false,
                        gridLines: false,
                        scaleLabel: {
                            display: true,
                            labelString: 'Month'
                        }
                    }],
                    yAxes: [{
                        display: false,
                        gridLines: false,
                        scaleLabel: {
                            display: true,
                            labelString: 'Value'
                        },
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                elements: {
                    line: {
                        tension: 0.0000001
                    },
                    point: {
                        radius: 4,
                        borderWidth: 12
                    }
                },
                layout: {
                    padding: {
                        left: 0,
                        right: 0,
                        top: 10,
                        bottom: 0
                    }
                }
            }
        };

        var chart = new Chart(ctx, config);
    }

    // Bandwidth Charts 2.
    // Based on Chartjs plugin - http://www.chartjs.org/
    var bandwidthChart2 = function() {
        if ($('#kt_chart_bandwidth2').length == 0) {
            return;
        }

        var ctx = document.getElementById("kt_chart_bandwidth2").getContext("2d");

        var gradient = ctx.createLinearGradient(0, 0, 0, 240);
        gradient.addColorStop(0, Chart.helpers.color('#ffefce').alpha(1).rgbString());
        gradient.addColorStop(1, Chart.helpers.color('#ffefce').alpha(0.3).rgbString());

        var config = {
            type: 'line',
            data: {
                labels: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October"],
                datasets: [{
                    label: "Bandwidth Stats",
                    backgroundColor: gradient,
                    borderColor: KTApp.getStateColor('warning'),
                    pointBackgroundColor: Chart.helpers.color('#000000').alpha(0).rgbString(),
                    pointBorderColor: Chart.helpers.color('#000000').alpha(0).rgbString(),
                    pointHoverBackgroundColor: KTApp.getStateColor('danger'),
                    pointHoverBorderColor: Chart.helpers.color('#000000').alpha(0.1).rgbString(),

                    //fill: 'start',
                    data: [
                        10, 14, 12, 16, 9, 11, 13, 9, 13, 15
                    ]
                }]
            },
            options: {
                title: {
                    display: false,
                },
                tooltips: {
                    mode: 'nearest',
                    intersect: false,
                    position: 'nearest',
                    xPadding: 10,
                    yPadding: 10,
                    caretPadding: 10
                },
                legend: {
                    display: false
                },
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    xAxes: [{
                        display: false,
                        gridLines: false,
                        scaleLabel: {
                            display: true,
                            labelString: 'Month'
                        }
                    }],
                    yAxes: [{
                        display: false,
                        gridLines: false,
                        scaleLabel: {
                            display: true,
                            labelString: 'Value'
                        },
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                elements: {
                    line: {
                        tension: 0.0000001
                    },
                    point: {
                        radius: 4,
                        borderWidth: 12
                    }
                },
                layout: {
                    padding: {
                        left: 0,
                        right: 0,
                        top: 10,
                        bottom: 0
                    }
                }
            }
        };

        var chart = new Chart(ctx, config);
    }

    // Bandwidth Charts 2.
    // Based on Chartjs plugin - http://www.chartjs.org/
    var adWordsStat = function() {
        if ($('#kt_chart_adwords_stats').length == 0) {
            return;
        }

        var ctx = document.getElementById("kt_chart_adwords_stats").getContext("2d");

        var gradient = ctx.createLinearGradient(0, 0, 0, 240);
        gradient.addColorStop(0, Chart.helpers.color('#ffefce').alpha(1).rgbString());
        gradient.addColorStop(1, Chart.helpers.color('#ffefce').alpha(0.3).rgbString());

        var config = {
            type: 'line',
            data: {
                labels: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October"],
                datasets: [{
                    label: "AdWord Clicks",
                    backgroundColor: KTApp.getStateColor('brand'),
                    borderColor: KTApp.getStateColor('brand'),

                    pointBackgroundColor: Chart.helpers.color('#000000').alpha(0).rgbString(),
                    pointBorderColor: Chart.helpers.color('#000000').alpha(0).rgbString(),
                    pointHoverBackgroundColor: KTApp.getStateColor('danger'),
                    pointHoverBorderColor: Chart.helpers.color('#000000').alpha(0.1).rgbString(),
                    data: [
                        12, 16, 9, 18, 13, 12, 18, 12, 15, 17
                    ]
                }, {
                    label: "AdWords Views",

                    backgroundColor: KTApp.getStateColor('success'),
                    borderColor: KTApp.getStateColor('success'),

                    pointBackgroundColor: Chart.helpers.color('#000000').alpha(0).rgbString(),
                    pointBorderColor: Chart.helpers.color('#000000').alpha(0).rgbString(),
                    pointHoverBackgroundColor: KTApp.getStateColor('danger'),
                    pointHoverBorderColor: Chart.helpers.color('#000000').alpha(0.1).rgbString(),
                    data: [
                        10, 14, 12, 16, 9, 11, 13, 9, 13, 15
                    ]
                }]
            },
            options: {
                title: {
                    display: false,
                },
                tooltips: {
                    mode: 'nearest',
                    intersect: false,
                    position: 'nearest',
                    xPadding: 10,
                    yPadding: 10,
                    caretPadding: 10
                },
                legend: {
                    display: false
                },
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    xAxes: [{
                        display: false,
                        gridLines: false,
                        scaleLabel: {
                            display: true,
                            labelString: 'Month'
                        }
                    }],
                    yAxes: [{
                        stacked: true,
                        display: false,
                        gridLines: false,
                        scaleLabel: {
                            display: true,
                            labelString: 'Value'
                        },
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                elements: {
                    line: {
                        tension: 0.0000001
                    },
                    point: {
                        radius: 4,
                        borderWidth: 12
                    }
                },
                layout: {
                    padding: {
                        left: 0,
                        right: 0,
                        top: 10,
                        bottom: 0
                    }
                }
            }
        };

        var chart = new Chart(ctx, config);
    }

    // Bandwidth Charts 2.
    // Based on Chartjs plugin - http://www.chartjs.org/
    var financeSummary = function() {
        if ($('#kt_chart_finance_summary').length == 0) {
            return;
        }

        var ctx = document.getElementById("kt_chart_finance_summary").getContext("2d");

        var config = {
            type: 'line',
            data: {
                labels: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October"],
                datasets: [{
                    label: "AdWords Views",

                    backgroundColor: KTApp.getStateColor('success'),
                    borderColor: KTApp.getStateColor('success'),

                    pointBackgroundColor: Chart.helpers.color('#000000').alpha(0).rgbString(),
                    pointBorderColor: Chart.helpers.color('#000000').alpha(0).rgbString(),
                    pointHoverBackgroundColor: KTApp.getStateColor('danger'),
                    pointHoverBorderColor: Chart.helpers.color('#000000').alpha(0.1).rgbString(),
                    data: [
                        10, 14, 12, 16, 9, 11, 13, 9, 13, 15
                    ]
                }]
            },
            options: {
                title: {
                    display: false,
                },
                tooltips: {
                    mode: 'nearest',
                    intersect: false,
                    position: 'nearest',
                    xPadding: 10,
                    yPadding: 10,
                    caretPadding: 10
                },
                legend: {
                    display: false
                },
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    xAxes: [{
                        display: false,
                        gridLines: false,
                        scaleLabel: {
                            display: true,
                            labelString: 'Month'
                        }
                    }],
                    yAxes: [{
                        display: false,
                        gridLines: false,
                        scaleLabel: {
                            display: true,
                            labelString: 'Value'
                        },
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                elements: {
                    line: {
                        tension: 0.0000001
                    },
                    point: {
                        radius: 4,
                        borderWidth: 12
                    }
                },
                layout: {
                    padding: {
                        left: 0,
                        right: 0,
                        top: 10,
                        bottom: 0
                    }
                }
            }
        };

        var chart = new Chart(ctx, config);
    }

    // Order Statistics.
    // Based on Chartjs plugin - http://www.chartjs.org/
    var orderStatistics = function() {
        var container = KTUtil.getByID('kt_chart_order_statistics');

        if (!container) {
            return;
        }

        var MONTHS = ['1 Jan', '2 Jan', '3 Jan', '4 Jan', '5 Jan', '6 Jan', '7 Jan'];

        var color = Chart.helpers.color;
        var barChartData = {
            labels: ['1 Jan', '2 Jan', '3 Jan', '4 Jan', '5 Jan', '6 Jan', '7 Jan'],
            datasets : [
				{
                    fill: true,
                    //borderWidth: 0,
                    backgroundColor: color(KTApp.getStateColor('brand')).alpha(0.6).rgbString(),
                    borderColor : color(KTApp.getStateColor('brand')).alpha(0).rgbString(),
                    
                    pointHoverRadius: 4,
                    pointHoverBorderWidth: 12,
                    pointBackgroundColor: Chart.helpers.color('#000000').alpha(0).rgbString(),
                    pointBorderColor: Chart.helpers.color('#000000').alpha(0).rgbString(),
                    pointHoverBackgroundColor: KTApp.getStateColor('brand'),
                    pointHoverBorderColor: Chart.helpers.color('#000000').alpha(0.1).rgbString(),

					data: [20, 30, 20, 40, 30, 60, 30]
				},
				{
                    fill: true,
                    //borderWidth: 0,
					backgroundColor : color(KTApp.getStateColor('brand')).alpha(0.2).rgbString(),
                    borderColor : color(KTApp.getStateColor('brand')).alpha(0).rgbString(),
                    
                    pointHoverRadius: 4,
                    pointHoverBorderWidth: 12,
                    pointBackgroundColor: Chart.helpers.color('#000000').alpha(0).rgbString(),
                    pointBorderColor: Chart.helpers.color('#000000').alpha(0).rgbString(),
                    pointHoverBackgroundColor: KTApp.getStateColor('brand'),
                    pointHoverBorderColor: Chart.helpers.color('#000000').alpha(0.1).rgbString(),

					data: [15, 40, 15, 30, 40, 30, 50]
				}
            ]
        };

        var ctx = container.getContext('2d');
        var chart = new Chart(ctx, {
            type: 'line',
            data: barChartData,
            options: {
                responsive: true,
                maintainAspectRatio: false,
                legend: false,
                scales: {
                    xAxes: [{
                        categoryPercentage: 0.35,
                        barPercentage: 0.70,
                        display: true,
                        scaleLabel: {
                            display: false,
                            labelString: 'Month'
                        },
                        gridLines: false,
                        ticks: {
                            display: true,
                            beginAtZero: true,
                            fontColor: KTApp.getBaseColor('shape', 3),
                            fontSize: 13,
                            padding: 10
                        }
                    }],
                    yAxes: [{
                        categoryPercentage: 0.35,
                        barPercentage: 0.70,
                        display: true,
                        scaleLabel: {
                            display: false,
                            labelString: 'Value'
                        },
                        gridLines: {
                            color: KTApp.getBaseColor('shape', 2),
                            drawBorder: false,
                            offsetGridLines: false,
                            drawTicks: false,
                            borderDash: [3, 4],
                            zeroLineWidth: 1,
                            zeroLineColor: KTApp.getBaseColor('shape', 2),
                            zeroLineBorderDash: [3, 4]
                        },
                        ticks: {
                            max: 70,                            
                            stepSize: 10,
                            display: true,
                            beginAtZero: true,
                            fontColor: KTApp.getBaseColor('shape', 3),
                            fontSize: 13,
                            padding: 10
                        }
                    }]
                },
                title: {
                    display: false
                },
                hover: {
                    mode: 'index'
                },
                tooltips: {
                    enabled: true,
                    intersect: false,
                    mode: 'nearest',
                    bodySpacing: 5,
                    yPadding: 10,
                    xPadding: 10, 
                    caretPadding: 0,
                    displayColors: false,
                    backgroundColor: KTApp.getStateColor('brand'),
                    titleFontColor: '#ffffff', 
                    cornerRadius: 4,
                    footerSpacing: 0,
                    titleSpacing: 0
                },
                layout: {
                    padding: {
                        left: 0,
                        right: 0,
                        top: 5,
                        bottom: 5
                    }
                }
            }
        });
    }

    // Quick Stat Charts
    var quickStats = function() {
        _initSparklineChart($('#kt_chart_quick_stats_1'), [10, 14, 18, 11, 9, 12, 14, 17, 18, 14], KTApp.getStateColor('brand'), 3);
        _initSparklineChart($('#kt_chart_quick_stats_2'), [11, 12, 18, 13, 11, 12, 15, 13, 19, 15], KTApp.getStateColor('danger'), 3);
        _initSparklineChart($('#kt_chart_quick_stats_3'), [12, 12, 18, 11, 15, 12, 13, 16, 11, 18], KTApp.getStateColor('success'), 3);
        _initSparklineChart($('#kt_chart_quick_stats_4'), [11, 9, 13, 18, 13, 15, 14, 13, 18, 15], KTApp.getStateColor('success'), 3);
    }

    // Daterangepicker Init
    var daterangepickerInit = function() {
        if ($('#kt_dashboard_daterangepicker').length == 0) {
            return;
        }

        var picker = $('#kt_dashboard_daterangepicker');
        var start = moment();
        var end = moment();

        function cb(start, end, label) {
            var title = '';
            var range = '';

            if ((end - start) < 100 || label == 'Hoy') {
                title = 'Hoy:';
                range = start.format('MMM D');
            } else if (label == 'Ayer') {
                title = 'Ayer:';
                range = start.format('MMM D');
            } else {
                range = start.format('MMM D') + ' - ' + end.format('MMM D');
            }

            $('#kt_dashboard_daterangepicker_date').html(range);
            $('#kt_dashboard_daterangepicker_title').html(title);
            //alert(start.format('YYYY/MM/DD'));
            $('#dsStartDate').val(start.format('YYYY/MM/DD'));
            $('#dsEndDate').val(end.format('YYYY/MM/DD'));
            //alert($('#dsStartDate').val());

        }

        picker.daterangepicker({
            direction: KTUtil.isRTL(),
            startDate: start,
            endDate: end,
            opens: 'left',
            ranges: {
                'Hoy': [moment(), moment()],
                'Ayer': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                'Últimos 7 Días': [moment().subtract(6, 'days'), moment()],
                'Últimos 30 Días': [moment().subtract(29, 'days'), moment()],
                'Este Mes': [moment().startOf('month'), moment().endOf('month')],
                'Mes Anterior': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
            }
        }, cb);

        cb(start, end, '');
    }


    // Calendar Init
    var calendarInit = function() {
        if ($('#kt_calendar').length === 0) {
            return;
        }
        
        var todayDate = moment().startOf('day');
        var YM = todayDate.format('YYYY-MM');
        var YESTERDAY = todayDate.clone().subtract(1, 'day').format('YYYY-MM-DD');
        var TODAY = todayDate.format('YYYY-MM-DD');
        var TOMORROW = todayDate.clone().add(1, 'day').format('YYYY-MM-DD');

        $('#kt_calendar').fullCalendar({
            isRTL: KTUtil.isRTL(),
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay,listWeek'
            },
            editable: true,
            eventLimit: true, // allow "more" link when too many events
            navLinks: true,
            defaultDate: moment('2017-09-15'),
            events: [
                {
                    title: 'Meeting',
                    start: moment('2017-08-28'),
                    description: 'Lorem ipsum dolor sit incid idunt ut',
                    className: "fc-event-light fc-event-solid-warning"
                },
                {
                    title: 'Conference',                    
                    description: 'Lorem ipsum dolor incid idunt ut labore',
                    start: moment('2017-08-29T13:30:00'),
                    end: moment('2017-08-29T17:30:00'),
                    className: "fc-event-success"
                },
                {
                    title: 'Dinner',
                    start: moment('2017-08-30'),
                    description: 'Lorem ipsum dolor sit tempor incid',
                    className: "fc-event-light  fc-event-solid-danger"
                },
                {
                    title: 'All Day Event',
                    start: moment('2017-09-01'),
                    description: 'Lorem ipsum dolor sit incid idunt ut',
                    className: "fc-event-danger fc-event-solid-focus"
                },
                {
                    title: 'Reporting',                    
                    description: 'Lorem ipsum dolor incid idunt ut labore',
                    start: moment('2017-09-03T13:30:00'),
                    end: moment('2017-09-04T17:30:00'),
                    className: "fc-event-success"
                },
                {
                    title: 'Company Trip',
                    start: moment('2017-09-05'),
                    end: moment('2017-09-07'),
                    description: 'Lorem ipsum dolor sit tempor incid',
                    className: "fc-event-primary"
                },
                {
                    title: 'ICT Expo 2017 - Product Release',
                    start: moment('2017-09-09'),
                    description: 'Lorem ipsum dolor sit tempor inci',
                    className: "fc-event-light fc-event-solid-primary"
                },
                {
                    title: 'Dinner',
                    start: moment('2017-09-12'),
                    description: 'Lorem ipsum dolor sit amet, conse ctetur'
                },
                {
                    id: 999,
                    title: 'Repeating Event',
                    start: moment('2017-09-15T16:00:00'),
                    description: 'Lorem ipsum dolor sit ncididunt ut labore',
                    className: "fc-event-danger"
                },
                {
                    id: 1000,
                    title: 'Repeating Event',
                    description: 'Lorem ipsum dolor sit amet, labore',
                    start: moment('2017-09-18T19:00:00'),
                },
                {
                    title: 'Conference',
                    start: moment('2017-09-20T13:00:00'),
                    end: moment('2017-09-21T19:00:00'),
                    description: 'Lorem ipsum dolor eius mod tempor labore',
                    className: "fc-event-success"
                },
                {
                    title: 'Meeting',
                    start: moment('2017-09-11'),
                    description: 'Lorem ipsum dolor eiu idunt ut labore'
                },
                {
                    title: 'Lunch',
                    start: moment('2017-09-18'),
                    className: "fc-event-info fc-event-solid-success",
                    description: 'Lorem ipsum dolor sit amet, ut labore'
                },
                {
                    title: 'Meeting',
                    start: moment('2017-09-24'),
                    className: "fc-event-warning",
                    description: 'Lorem ipsum conse ctetur adipi scing'
                },
                {
                    title: 'Happy Hour',
                    start: moment('2017-09-24'),
                    className: "fc-event-light fc-event-solid-focus",
                    description: 'Lorem ipsum dolor sit amet, conse ctetur'
                },
                {
                    title: 'Dinner',
                    start: moment('2017-09-24'),
                    className: "fc-event-solid-focus fc-event-light",
                    description: 'Lorem ipsum dolor sit ctetur adipi scing'
                },
                {
                    title: 'Birthday Party',
                    start: moment('2017-09-24'),
                    className: "fc-event-primary",
                    description: 'Lorem ipsum dolor sit amet, scing'
                },
                {
                    title: 'Company Event',
                    start: moment('2017-09-24'),
                    className: "fc-event-danger",
                    description: 'Lorem ipsum dolor sit amet, scing'
                },
                {
                    title: 'Click for Google',
                    url: 'http://google.com/',
                    start: moment('2017-09-26'),
                    className: "fc-event-solid-info fc-event-light",
                    description: 'Lorem ipsum dolor sit amet, labore'
                }
            ],

            eventRender: function(event, element) {
                if (element.hasClass('fc-day-grid-event')) {
                    element.data('content', event.description);
                    element.data('placement', 'top');
                    KTApp.initPopover(element);
                } else if (element.hasClass('fc-time-grid-event')) {
                    element.find('.fc-title').append('<div class="fc-description">' + event.description + '</div>');
                } else if (element.find('.fc-list-item-title').lenght !== 0) {
                    element.find('.fc-list-item-title').append('<div class="fc-description">' + event.description + '</div>');
                }
            }
        });
    }

    // Earnings Sliders
    var earningsSlide = function() {
        var carousel1 = $('#kt_earnings_widget .kt-widget30__head .owl-carousel');
        var carousel2 = $('#kt_earnings_widget .kt-widget30__body .owl-carousel');

        carousel1.find('.carousel').each( function( index ) {
            $(this).attr( 'data-position', index );
        });

        carousel1.owlCarousel({
            rtl: KTUtil.isRTL(),
            center: true,
            loop: true,
            items: 2
        });

        carousel2.owlCarousel({
            rtl: KTUtil.isRTL(),
            items: 1,
            animateIn: 'fadeIn(100)',
            loop: true
        });

        $(document).on('click', '.carousel', function() {
            var index = $(this).attr( 'data-position' );
            if (index) {
                carousel1.trigger('to.owl.carousel', index );
                carousel2.trigger('to.owl.carousel', index );
            }
        });

        carousel1.on('changed.owl.carousel', function () {
            var index = $(this).find('.owl-item.active.center').find('.carousel').attr('data-position');
            if (index) {
                carousel2.trigger('to.owl.carousel', index);
            }
        });

        carousel2.on('changed.owl.carousel', function () {
            var index = $(this).find('.owl-item.active.center').find('.carousel').attr('data-position');
            if (index){
                carousel1.trigger('to.owl.carousel', index);
            }
        });

        
    }

    //Uppy
    const Tus = Uppy.Tus;
	const ProgressBar = Uppy.ProgressBar;
	const StatusBar = Uppy.StatusBar;
	const FileInput = Uppy.FileInput;
    const Informer = Uppy.Informer;
    
    var initUppy5 = function(){
		// Uppy variables
        // For more info refer: https://uppy.io/
		var elemId = 'kt_uppy_5';
		var id = '#' + elemId;
		var $statusBar = $(id + ' .kt-uppy__status');
		var $uploadedList = $(id + ' .kt-uppy__list');
		var timeout;
		
		var uppyMin = Uppy.Core({
			debug: true, 
			autoProceed: true,
			showProgressDetails: true,
			restrictions: {
				maxFileSize: 1000000, // 1mb
				maxNumberOfFiles: 1,
				minNumberOfFiles: 1
			}
		});
		
		uppyMin.use(FileInput, { target: id + ' .kt-uppy__wrapper', pretty: false });
		uppyMin.use(Informer, { target: id + ' .kt-uppy__informer'  });

		// demo file upload server
		uppyMin.use(Tus, { endpoint: 'https://master.tus.io/files/' });
		uppyMin.use(StatusBar, {
			target: id + ' .kt-uppy__status',
			hideUploadButton: true,
			hideAfterFinish: false
		});

		$(id + ' .uppy-FileInput-input').addClass('kt-uppy__input-control').attr('id', elemId + '_input_control');
		$(id + ' .uppy-FileInput-container').append('<label class="kt-uppy__input-label btn btn-label-brand btn-bold btn-font-sm" for="' + (elemId + '_input_control') + '">Subir XML</label>');
		
		var $fileLabel = $(id + ' .kt-uppy__input-label');

		uppyMin.on('upload', function(data) {
			$fileLabel.text("Subiendo...");
			$statusBar.addClass('kt-uppy__status--ongoing');
			$statusBar.removeClass('kt-uppy__status--hidden');
			clearTimeout( timeout );
		});

		uppyMin.on('complete', function(file) {
			$.each(file.successful, function(index, value){
				var sizeLabel = "bytes";
				var filesize = value.size;
				if (filesize > 1024){
					filesize = filesize / 1024;
					sizeLabel = "kb";

					if(filesize > 1024){
						filesize = filesize / 1024;
						sizeLabel = "MB";
					}
				}					
				var uploadListHtml = '<div class="kt-uppy__list-item" data-id="'+value.id+'"><div class="kt-uppy__list-label">'+value.name+' ('+ Math.round(filesize, 2) +' '+sizeLabel+')</div><span class="kt-uppy__list-remove" data-id="'+value.id+'"><i class="flaticon2-cancel-music"></i></span></div>';
				$uploadedList.append(uploadListHtml);
			});

			$fileLabel.text("Agregar más");		

			$statusBar.addClass('kt-uppy__status--hidden');
			$statusBar.removeClass('kt-uppy__status--ongoing');
		});

		$(document).on('click', id + ' .kt-uppy__list .kt-uppy__list-remove', function(){
			var itemId = $(this).attr('data-id');
			uppyMin.removeFile(itemId);
			$(id + ' .kt-uppy__list-item[data-id="'+itemId+'"').remove();
		});
    }

    // Class definition
var KTChat = function () {
	var initChat = function (parentEl) {
		var messageListEl = KTUtil.find(parentEl, '.kt-scroll');

		if (!messageListEl) {
			return;
		}

		// initialize perfect scrollbar(see:  https://github.com/utatti/perfect-scrollbar)
		KTUtil.scrollInit(messageListEl, {
			windowScroll: false, // allow browser scroll when the scroll reaches the end of the side
			mobileNativeScroll: true,  // enable native scroll for mobile
			desktopNativeScroll: false, // disable native scroll and use custom scroll for desktop
			resetHeightOnDestroy: true,  // reset css height on scroll feature destroyed
			handleWindowResize: true, // recalculate hight on window resize
			rememberPosition: true, // remember scroll position in cookie
			height: function() {  // calculate height
				var height;

				// Mobile mode
				if (KTUtil.isInResponsiveRange('tablet-and-mobile')) {
					return KTUtil.hasAttr(messageListEl, 'data-mobile-height') ? parseInt(KTUtil.attr(messageListEl, 'data-mobile-height')) : 300;
				}

				// Desktop mode
				if (KTUtil.isInResponsiveRange('desktop') && KTUtil.hasAttr(messageListEl, 'data-height')) {
					return parseInt(KTUtil.attr(messageListEl, 'data-height'));
				}

				var chatEl = KTUtil.find(parentEl, '.kt-chat');
				var portletHeadEl = KTUtil.find(parentEl, '.kt-portlet > .kt-portlet__head');
				var portletBodyEl = KTUtil.find(parentEl, '.kt-portlet > .kt-portlet__body');
				var portletFootEl = KTUtil.find(parentEl, '.kt-portlet > .kt-portlet__foot');

				if (KTUtil.isInResponsiveRange('desktop')) {
					height = KTLayout.getContentHeight();
				} else {
					height = KTUtil.getViewPort().height;
				}

				if (chatEl) {
					height = height - parseInt(KTUtil.css(chatEl, 'margin-top')) - parseInt(KTUtil.css(chatEl, 'margin-bottom'));
					height = height - parseInt(KTUtil.css(chatEl, 'padding-top')) - parseInt(KTUtil.css(chatEl, 'padding-bottom'));
				}

				if (portletHeadEl) {
					height = height - parseInt(KTUtil.css(portletHeadEl, 'height'));
					height = height - parseInt(KTUtil.css(portletHeadEl, 'margin-top')) - parseInt(KTUtil.css(portletHeadEl, 'margin-bottom'));
				}

				if (portletBodyEl) {
					height = height - parseInt(KTUtil.css(portletBodyEl, 'margin-top')) - parseInt(KTUtil.css(portletBodyEl, 'margin-bottom'));
					height = height - parseInt(KTUtil.css(portletBodyEl, 'padding-top')) - parseInt(KTUtil.css(portletBodyEl, 'padding-bottom'));
				}

				if (portletFootEl) {
					height = height - parseInt(KTUtil.css(portletFootEl, 'height'));
					height = height - parseInt(KTUtil.css(portletFootEl, 'margin-top')) - parseInt(KTUtil.css(portletFootEl, 'margin-bottom'));
				}

				// remove additional space
				height = height - 5;

				return height;
			}
		});

		// messaging
		var handleMessaging = function() {
			var scrollEl = KTUtil.find(parentEl, '.kt-scroll');
			var messagesEl = KTUtil.find(parentEl, '.kt-chat__messages');
            var textarea = KTUtil.find(parentEl, '.kt-chat__input textarea');

            if (textarea.value.length === 0 ) {
                return;
            }

			var node = document.createElement("DIV");
			KTUtil.addClass(node, 'kt-chat__message kt-chat__message--brand kt-chat__message--right');

			var html =
				'<div class="kt-chat__user">' +
                    '<div class="kt-badge kt-badge--md kt-badge--brand">'+$("#userbadge").html()+'</div>'+
                    '<a href="#" class="kt-chat__username">'+$("#username").html()+'</span></a>' +
                    '<span class="kt-chat__datetime">Ahora</span>' +
				'</div>' +
				'<div class="kt-chat__text kt-bg-light-brand">' +
					textarea.value
                '</div>';
                
            var urlMethod = "GetHint.aspx/InsertChat";
            var usrId= $("#userid").val();
            var jsonData = "{'userid': '"+usrId+"', 'message':'"+textarea.value+"' }";    
            SendAjax(urlMethod,jsonData,ReturnInsChat);

			KTUtil.setHTML(node, html);
			messagesEl.appendChild(node);
			textarea.value = '';
			scrollEl.scrollTop = parseInt(KTUtil.css(messagesEl, 'height'));
			var ps;
			if (ps = KTUtil.data(scrollEl).get('ps')) {
				ps.update();
			}

			// setTimeout(function() {
			// 	var node = document.createElement("DIV");
			// 	KTUtil.addClass(node, 'kt-chat__message kt-chat__message--success');

			// 	var html =
			// 		'<div class="kt-chat__user">' +
			// 			'<span class="kt-media kt-media--circle kt-media--sm">' +
			// 				'<img src="./assets/media/users/100_13.jpg" alt="image">'  +
			// 			'</span>' +
			// 			'<a href="#" class="kt-chat__username">Encargado</span></a>' +
			// 			'<span class="kt-chat__datetime">Just now</span>' +
			// 		'</div>' +
			// 		'<div class="kt-chat__text kt-bg-light-success">' +
			// 		'Right' +
			// 		'</div>';

			// 	KTUtil.setHTML(node, html);
			// 	messagesEl.appendChild(node);
			// 	textarea.value = '';
			// 	scrollEl.scrollTop = parseInt(KTUtil.css(messagesEl, 'height'));

			// 	var ps;
			// 	if (ps = KTUtil.data(scrollEl).get('ps')) {
			// 		ps.update();
			// 	}
			// }, 2000);
		}

		// attach events
		KTUtil.on(parentEl, '.kt-chat__input textarea', 'keydown', function(e) {
			if (e.keyCode == 13) {
				handleMessaging();
				e.preventDefault();

				return false;
			}
		});

		KTUtil.on(parentEl, '.kt-chat__input .kt-chat__reply', 'click', function(e) {
			handleMessaging();
		});
	}

	return {
		// public functions
		init: function() {
			// init modal chat example
			initChat( KTUtil.getByID('kt_chat_modal'));

			// trigger click to show popup modal chat on page load
			if (encodeURI(window.location.hostname) == 'keenthemes.com' || encodeURI(window.location.hostname) == 'www.keenthemes.com') {
				setTimeout(function() {
		            if (!Cookies.get('kt_app_chat_shown')) {
		                var expires = new Date(new Date().getTime() + 60 * 60 * 1000); // expire in 60 minutes from now
		                Cookies.set('kt_app_chat_shown', 1, { expires: expires });
		                KTUtil.getByID('kt_app_chat_launch_btn').click();
		            }
		        }, 2000);
	        }
        },

        setup: function(element) {
            initChat(element);
        }
	};
}();

// webpack support
if (typeof module !== 'undefined') {
	module.exports = KTChat;
}

KTUtil.ready(function() {
	KTChat.init();
});


    var demo1 = function () {
        // set the dropzone container id
        var id = '#kt_dropzone_1';

        // set the preview element template
        var previewNode = $(id + " .dropzone-item");
        previewNode.id = "";
        var previewTemplate = previewNode.parent('.dropzone-items').html();
        previewNode.remove();

        var myDropzone4 = new Dropzone(id, { // Make the whole body a dropzone
            url: "UploadFile.ashx", // Set the url for your upload script location
            parallelUploads: 20,
            previewTemplate: previewTemplate,
            maxFilesize: 1, // Max filesize in MB
            autoQueue: true, // Make sure the files aren't queued until manually added
            previewsContainer: id + " .dropzone-items", // Define the container to display the previews
            clickable: id + " .dropzone-select" // Define the element that should be used as click trigger to select files.
        });

        myDropzone4.on("addedfile", function(file) {
            KTApp.block('#docuprow', {
                overlayColor: '#000000',
                type: 'v2',
                state: 'primary',
                message: 'Cargando archivo, espere un momento...'
            });
            // Hookup the start button
            file.previewElement.querySelector(id + " .dropzone-start").onclick = function() { myDropzone4.enqueueFile(file); };
            $(document).find( id + ' .dropzone-item').css('display', '');
            $( id + " .dropzone-upload, " + id + " .dropzone-remove-all").css('display', 'inline-block');
            //myDropzone4.processQueue();
        });

        // Update the total progress bar
        myDropzone4.on("totaluploadprogress", function(progress) {
            $(this).find( id + " .progress-bar").css('width', progress + "%");
        });

        myDropzone4.on("sending", function(file,xhr,data) {
            // Show the total progress bar when upload starts
            data.append("po", $('select#cmbPchOrder').val());
            $( id + " .progress-bar").css('opacity', '1');
            // And disable the start button
            file.previewElement.querySelector(id + " .dropzone-start").setAttribute("disabled", "disabled");
        });

        // Hide the total progress bar when nothing's uploading anymore
        myDropzone4.on("complete", function(progress) {
            var thisProgressBar = id + " .dz-complete";
            setTimeout(function(){
                $( thisProgressBar + " .progress-bar, " + thisProgressBar + " .progress, " + thisProgressBar + " .dropzone-start").css('opacity', '0');
            }, 300)
            
            var res = progress.xhr.response.split(",");
            $(id + " .dropzone-error").html(res[1]);
            $(id + " .dropzone-error").css('display', 'block');
            if(res[0]=="success")
            {
                $("#dropzone-error1").removeClass("dropzone-error");
                $("#dropzone-error1").addClass("dropzone-filename");
                swal.fire("Recibido", "El archivo fue recibido con éxito", "success");
            }
            else
            {
                swal.fire("No se pudo subir",res[1], "error");
                $("#dropzone-error1").addClass("dropzone-error");
                $("#dropzone-error1").removeClass("dropzone-filename");
            }
            KTApp.unblock('#docuprow');
            console.log(progress.xhr.response);
        });

        // Setup the buttons for all transfers
        document.querySelector( id + " .dropzone-upload").onclick = function() {
            myDropzone4.enqueueFiles(myDropzone4.getFilesWithStatus(Dropzone.ADDED));
        };

        // Setup the button for remove all files
        document.querySelector(id + " .dropzone-remove-all").onclick = function() {
            $( id + " .dropzone-upload, " + id + " .dropzone-remove-all").css('display', 'none');
            myDropzone4.removeAllFiles(true);
        };

        // On all files completed upload
        myDropzone4.on("queuecomplete", function(progress){
            $( id + " .dropzone-upload").css('display', 'none');
        });

        // On all files removed
        myDropzone4.on("removedfile", function(file){
            if(myDropzone4.files.length < 1){
                $( id + " .dropzone-upload, " + id + " .dropzone-remove-all").css('display', 'none');
            }
        });
    }
    
    var demo2 = function () {
        // set the dropzone container id
        var id = '#kt_dropzone_4';

        // set the preview element template
        var previewNode = $(id + " .dropzone-item");
        previewNode.id = "";
        var previewTemplate = previewNode.parent('.dropzone-items').html();
        previewNode.remove();

        var myDropzone4 = new Dropzone(id, { // Make the whole body a dropzone
            url: "UploadFile.ashx", // Set the url for your upload script location
            parallelUploads: 20,
            previewTemplate: previewTemplate,
            maxFilesize: 1, // Max filesize in MB
            autoQueue: true, // Make sure the files aren't queued until manually added
            previewsContainer: id + " .dropzone-items", // Define the container to display the previews
            clickable: id + " .dropzone-select" // Define the element that should be used as click trigger to select files.
        });

        myDropzone4.on("addedfile", function(file) {
            //var fle=JSON.parse(file);
            console.log(file.upload.filename);
            var mssg="Cargando archivo, espere un momento...";
            if(file.upload.filename.includes(".xml"))
                mssg="Revisando estatus con el SAT...";
            KTApp.block('#invuprow', {
                overlayColor: '#000000',
                type: 'v2',
                state: 'primary',
                message: mssg
            });
            // Hookup the start button
            file.previewElement.querySelector(id + " .dropzone-start").onclick = function() { myDropzone4.enqueueFile(file); };
            $(document).find( id + ' .dropzone-item').css('display', '');
            $( id + " .dropzone-upload, " + id + " .dropzone-remove-all").css('display', 'inline-block');
            
            //myDropzone4.processQueue();
        });

        // Update the total progress bar
        myDropzone4.on("totaluploadprogress", function(progress) {
            $(this).find( id + " .progress-bar").css('width', progress + "%");
        });

        myDropzone4.on("sending", function(file,xhr,data) {
            // Show the total progress bar when upload starts
            data.append("po", $('select#cmbPchOrder').val());
            $( id + " .progress-bar").css('opacity', '1');
            // And disable the start button
            file.previewElement.querySelector(id + " .dropzone-start").setAttribute("disabled", "disabled");
        });

        // Hide the total progress bar when nothing's uploading anymore
        myDropzone4.on("complete", function(progress) {
            var thisProgressBar = id + " .dz-complete";
            setTimeout(function(){
                $( thisProgressBar + " .progress-bar, " + thisProgressBar + " .progress, " + thisProgressBar + " .dropzone-start").css('opacity', '0');
            }, 300)
            

            var res = progress.xhr.response.split(",");
            $(id + " .dropzone-error").html(res[1]);
            $(id + " .dropzone-error").css('display', 'block');
            if(res[0]=="success")
            {
                $("#dropzone-error").removeClass("dropzone-error");
                $("#dropzone-error").addClass("dropzone-filename");
                $("#btnUpldXml").html("Subir PDF");
                swal.fire("Recibido", "El archivo fue recibido con éxito", "success");

            }
            else
            {
                swal.fire("No se pudo subir",res[1], "error");
                $("#dropzone-error").addClass("dropzone-error");
                $("#dropzone-error").removeClass("dropzone-filename");
            }
            KTApp.unblock('#invuprow');
            console.log(progress.xhr.response);
        });

        // Setup the buttons for all transfers
        document.querySelector( id + " .dropzone-upload").onclick = function() {
            myDropzone4.enqueueFiles(myDropzone4.getFilesWithStatus(Dropzone.ADDED));
        };

        // Setup the button for remove all files
        document.querySelector(id + " .dropzone-remove-all").onclick = function() {
            $( id + " .dropzone-upload, " + id + " .dropzone-remove-all").css('display', 'none');
            myDropzone4.removeAllFiles(true);
        };

        // On all files completed upload
        myDropzone4.on("queuecomplete", function(progress){
            $( id + " .dropzone-upload").css('display', 'none');
        });

        // On all files removed
        myDropzone4.on("removedfile", function(file){
            if(myDropzone4.files.length < 1){
                $( id + " .dropzone-upload, " + id + " .dropzone-remove-all").css('display', 'none');
            }
        });
    }

    var frepeater = function() {
        $('#kt_repeater_1').repeater({
            initEmpty: false,
           
            defaultValues: {
                'text-input': 'foo',
                'lntax' : '16',
                'lndiscount' : '0'
            },
             
            show: function() {
                $(this).slideDown();                               
            },

            hide: function(deleteElement) {                 
                //if(confirm('Seguro que quieres eliminar el registro?')) {
                //    $(this).slideUp(deleteElement);
                //}       
                Swal.fire({
                    title: 'Eliminar registro',
                    text: "Seguro quieres eliminar el registro",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonText: 'Aceptar',
                    customClass: {
                        confirmButton: 'btn btn-brand',
                        cancelButton: 'btn btn-danger'
                    },
                    cancelButtonText: 'Cancelar'
                  }).then((result) => {
                    if (result.value) {
                        $(this).slideUp(deleteElement);
                    }
                  })                         
            }      
        });
    }

    return {
        // Init demos
        init: function() {
            // init charts
            //dailySales();
            //profitShare();
            //salesStats();
            //salesByApps();
            //latestUpdates();
            //trendsStats();
            //trendsStats2();
            //latestTrendsMap();
            //revenueChange();
            //supportCases();
            //supportRequests();
            //activitiesChart();
            //bandwidthChart1();
            //bandwidthChart2();
            //adWordsStat();
            //financeSummary();
            //quickStats();
            //orderStatistics();

            // init daterangepicker
            daterangepickerInit();

            // datatables
            //datatableLatestOrders({});

            // calendar
            calendarInit();

            // earnings slide
            earningsSlide();

            //initUppy5();
            demo1();
            demo2();

            //repeater
            frepeater();

            // demo loading
            var loading = new KTDialog({'type': 'loader', 'placement': 'top center', 'message': 'Cargando ...'});
            loading.show();

            setTimeout(function() {
                loading.hide();
            }, 500);
        }
    };
}();

function clearUploads()
{
    $("#kt_dropzone_4 .dropzone-remove-all").click();
    console.log("click");
}

// Latest Orders
function LatestOrders(array) {
    if ($('#kt_datatable_latest_orders').length === 0) {
        return;
    }

    var dataJSONArray = array

    var datatable = $('.kt-datatable').KTDatatable({
        data: {
            type: 'local',
            source: dataJSONArray,
            pageSize: 10,
            saveState: {
                cookie: false,
                webstorage: true
            },
            serverPaging: false,
            serverFiltering: false,
            serverSorting: false
        },

        layout: {
            scroll: true,
            height: 500,
            footer: false
        },

        sortable: true,

        filterable: true,

        pagination: true,

        columns: [{
            field: "crm_quote_id",
            title: "#",
            sortable: false,
            width: 40,
            selector: {
                class: 'kt-checkbox--solid'
            },
            textAlign: 'center'
        }, {
            field: "name",
            title: "Nombre",
            width: 'auto',
            autoHide: false,
            // callback function support for column rendering
            template: function(data, i) {
              var number = i + 1;
              while (number > 5) {
                number = number - 3;
              }
                var img = number + '.png';

                var skills = [
                    'Angular, React',
                    'Vue, Kendo', 
                    '.NET, Oracle, MySQL',
                    'Node, SASS, Webpack',
                    'MangoDB, Java',
                    'HTML5, jQuery, CSS3'
                ];

                var output = '\
                    <div class="kt-user-card-v2">\
                        <div class="kt-user-card-v2__pic">\
                            <img src="assets/media/client-logos/logo' + img + '" alt="photo">\
                        </div>\
                        <div class="kt-user-card-v2__details">\
                            <a href="#" class="kt-user-card-v2__name">' + data.name + '</a>\
                            <span class="kt-user-card-v2__email">' +
                            data.description + '</span>\
                        </div>\
                    </div>';

                return output;
            }
        }, {
            field: "created",
            title: "Fecha",
            width: 100,
            type: "date",
            format: 'MM/DD/YYYY',
            template: function(data) {
                return '<span class="kt-font-bold">' + data.created + '</span>';
            }
        }, {
            field: "closed",
            title: "Status",
            width: 100,
            // callback function support for column rendering
            template: function(row) {
                var status = {
                    1: {
                        'title': 'Cerrada',
                        'class': ' btn-label-brand'
                    },
                    2: {
                        'title': 'Processing',
                        'class': ' btn-label-danger'
                    },
                    3: {
                        'title': 'Success',
                        'class': ' btn-label-success'
                    },                                                
                    4: {
                        'title': 'Delivered',
                        'class': ' btn-label-success'
                    },
                    5: {
                        'title': 'Canceled',
                        'class': ' btn-label-warning'
                    },
                    6: {
                        'title': 'Done',
                        'class': ' btn-label-danger'
                    },
                    7: {
                        'title': 'On Hold',
                        'class': ' btn-label-warning'
                    }
                };
                return '<span class="btn btn-bold btn-sm btn-font-sm ' + status[row.Status].class + '">' + status[row.Status].title + '</span>';
            }
        }, {
            field: "prospect_id",
            title: "Managed By",
            width: 200,
            // callback function support for column rendering
            template: function(data, i) {
                var number = 4 + i;
                while (number > 12) {
                    number = number - 3;
                }
                var user_img = '100_' + number + '.jpg';
                
                var pos = KTUtil.getRandomInt(0, 5);
                var position = [
                    'Developer',
                    'Designer', 
                    'CEO',
                    'Manager',
                    'Architect',
                    'Sales'
                ];

                var output = '';
                if (number > 5) {
                    output = '<div class="kt-user-card-v2">\
                        <div class="kt-user-card-v2__pic">\
                            <img src="assets/media/users/' + user_img + '" alt="photo">\
                        </div>\
                        <div class="kt-user-card-v2__details">\
                            <a href="#" class="kt-user-card-v2__name">' + data.prospect_id + '</a>\
                            <span class="kt-user-card-v2__desc">' + position[pos] + '</span>\
                        </div>\
                    </div>';
                }
                else {
                    var stateNo = KTUtil.getRandomInt(0, 6);
                    var states = [
                        'success',
                        'brand',
                        'danger',
                        'success',
                        'warning',
                        'primary',
                        'info'];
                    var state = states[stateNo];

                    output = '<div class="kt-user-card-v2">\
                        <div class="kt-user-card-v2__pic">\
                            <div class="kt-badge kt-badge--xl kt-badge--' + state + '">' + data.prospect_id.substring(0, 1) + '</div>\
                        </div>\
                        <div class="kt-user-card-v2__details">\
                            <a href="#" class="kt-user-card-v2__name">' + data.prospect_id + '</a>\
                            <span class="kt-user-card-v2__desc">' + position[pos] + '</span>\
                        </div>\
                    </div>';
                }

                return output;
            }
        }, {
            field: "Actions",
            width: 80,
            title: "Actions",
            sortable: false,
            autoHide: false,
            overflow: 'visible',
            template: function() {
                return '\
                    <div class="dropdown">\
                        <a href="javascript:;" class="btn btn-sm btn-clean btn-icon btn-icon-md" data-toggle="dropdown">\
                            <i class="flaticon-more-1"></i>\
                        </a>\
                        <div class="dropdown-menu dropdown-menu-right">\
                            <ul class="kt-nav">\
                                <li class="kt-nav__item">\
                                    <a href="#" onclick="getDSource();" class="kt-nav__link">\
                                        <i class="kt-nav__link-icon flaticon2-expand"></i>\
                                        <span class="kt-nav__link-text">View</span>\
                                    </a>\
                                </li>\
                                <li class="kt-nav__item">\
                                    <a href="#" class="kt-nav__link">\
                                        <i class="kt-nav__link-icon flaticon2-contract"></i>\
                                        <span class="kt-nav__link-text">Edit</span>\
                                    </a>\
                                </li>\
                                <li class="kt-nav__item">\
                                    <a href="#" class="kt-nav__link">\
                                        <i class="kt-nav__link-icon flaticon2-trash"></i>\
                                        <span class="kt-nav__link-text">Delete</span>\
                                    </a>\
                                </li>\
                                <li class="kt-nav__item">\
                                    <a href="#" class="kt-nav__link">\
                                        <i class="kt-nav__link-icon flaticon2-mail-1"></i>\
                                        <span class="kt-nav__link-text">Export</span>\
                                    </a>\
                                </li>\
                            </ul>\
                        </div>\
                    </div>\
                ';
            }
        }]
    });
    
}



function updateInvoice(pID) {

    KTApp.block('#invrow', {
        overlayColor: '#000000',
        type: 'v2',
        state: 'primary',
        message: 'Actualizando y enviando correo...'
    });
    var urlMethod = "GetHint.aspx/UpdateInvoice";
    $("#pID").val(pID);
    var jsonData = "{form: " + JSON.stringify($("#forma_reporte").serializeArray()) + "}";
    SendAjax(urlMethod, jsonData, ReturnUpdate);
    
}
function ReturnUpdate(msg) {
    var listItems = "";
   
    swal.fire("Actualizado", "Datos Almacenados con éxito", "success");
    // $.notify({
    //     // options
    //     message: 'Probando' 
    // },{
    //     // settings
    //     type: 'success'
    // });
    $('#actionDismiss').click();
    KTApp.unblock('#invrow');
    getQuotes(false);
}


function setpInv(pInv){
    $("#pinvID").val(pInv);
}

function updateSupplier(supId)
{
    $("#supID").val(supId);
    var urlMethod = "GetHint.aspx/UpdateSupplier";
    var jsonData = "{form: " + JSON.stringify($("#supForm").serializeArray()) + "}";
    SendAjax(urlMethod, jsonData, ReturnUpdateSupplier);
}

function ReturnInsChat(msd)
{

}
function ReturnUpdateSupplier(msg) {
    var listItems = "";
    var resp = JSON.parse(msg.d);
    if(resp.response=="success")
    {
        $('#sup'+resp.message).hide();
        swal.fire("Autorizado", "Datos Almacenados con éxito", "success");
    }
    else{
        swal.fire("Problemas", resp.message, "error");
        console.log(resp.error);
    }
    
    
}


function authorizeRange(supId)
{
    //$("#supID").val(supId);
    KTApp.block('#auth_form', {
        overlayColor: '#000000',
        type: 'v2',
        state: 'primary',
        message: 'Autorizando...'
    });
    var urlMethod = "GetHint.aspx/AuthorizeRange";
    var jsonData = "{rangeid: \"" + supId + "\"}";
    SendAjax(urlMethod, jsonData, ReturnAuthorizeRange);
}


function ReturnAuthorizeRange(msg) {
    var listItems = "";
    var resp = JSON.parse(msg.d);
    if(resp.response=="success")
    {
        swal.fire("Autorizado", "Datos Almacenados con éxito", "success");
       
        getRanges(false);
    }
    else{
        swal.fire("Problemas", resp.message, "error");
        console.log(resp.error);
    }
    KTApp.unblock('#auth_form');
    
}

function updatePay()
{
    KTApp.block('#invrow', {
        overlayColor: '#000000',
        type: 'v2',
        state: 'primary',
        message: 'Actualizando y enviando correo...'
    });
    var urlMethod = "GetHint.aspx/UpdateInvoice";
    var jsonData = "{form: " + JSON.stringify($("#payDate").serializeArray()) + "}";
    SendAjax(urlMethod, jsonData, ReturnUpdate);
}

function convertPDF(uuid){
    var urlMethod = "GetHint.aspx/ConvertPDF";
    var jsonData = "{uuid:\""+uuid+"\"}";
    SendAjax(urlMethod, jsonData, returnConvertPDF); 
}

function returnConvertPDF(msg){
    //window.open(msg.d,'Download');
    var link=document.createElement('a');
    var filePath= msg.d;
    link.href = filePath;
    link.download = filePath.substr(filePath.lastIndexOf('/') + 1);
    link.click();
}

function downloadRecOrder(uuid){
    var urlMethod = "GetHint.aspx/RecOrder";
    var jsonData = "{uuid:\""+uuid+"\"}";
    SendAjax(urlMethod, jsonData, returnDownloadRecOrder); 
}

function returnDownloadRecOrder(msg){
    //window.open(msg.d,'Download');
    var link=document.createElement('a');
    var filePath= msg.d;
    link.href = filePath;
    link.download = filePath.substr(filePath.lastIndexOf('/') + 1);
    link.click();
}

function saveInvoice(){

    var form = $( "#invoice_form" );
    form.validate({
        // define validation rules
        ignore: "",
        rules: {
            fullname: {
                required: true
            },
            rfc: {
                required: true,
                minlength: 12
            },
            email: {
                required: false
            },
            pay_method:{
                required: true
            },
            pay_method_cond:{
                required: true
            },
            state:{
                required: true
            },
            city:{
                required: true
            },
            postal_code:{
                required: true  
            },
            exchange_rate:{
                required: true  
            },
            cfdi_usage:{
                required: true
            },
            pay_cond:{
                required: true
            }
        }
    });  

    var doReturn=false;
    if($("#pay_method").val()==-1){
        $("#pay_method").parent().addClass("is-invalid"); doReturn=true;
    }
    if($("#cfdi_usage").val()==-1){
       $("#cfdi_usage").parent().addClass("is-invalid"); doReturn=true;
   }
   if($("#pay_method_cond").val()==-1){
       $("#pay_method_cond").parent().addClass("is-invalid"); doReturn=true;
   }
   if($("#state").val()==-1){
       $("#state").parent().addClass("is-invalid"); doReturn=true;
   }
   if($("#city").val()==-1){
       $("#city").parent().addClass("is-invalid"); doReturn=true;
   }
    if(!form.valid() | doReturn)
    {
        var alert = $('#kt_form_1_msg');
        alert.removeClass('kt--hide').show();
        KTUtil.scrollTo('kt_form_1_msg', -200);
        return;
    }
    
    Swal.fire({
        title: 'Facturar',
        text: "Los datos son correctos?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Aceptar',
        customClass: {
            confirmButton: 'btn btn-brand',
            cancelButton: 'btn btn-danger'
        },
        cancelButtonText: 'Cancelar'
      }).then((result) => {
        if (result.value) {
            $("#statename").val($("#state option:selected").text());
            $("#cityname").val($("#city option:selected").text());
            console.log($("#statename").val() + $("#cityname").val());
            KTApp.block('#invoice_form', {
                overlayColor: '#000000',
                type: 'v2',
                state: 'primary',
                message: 'Guardando Factura y generando PDF...'
            });
            var urlMethod = "GetHint.aspx/SaveInvoice";
            var jsonData = "{form: " + JSON.stringify($("#invoice_form").serializeArray()) + "}";
            SendAjax(urlMethod, jsonData, returnSaveInvoice); 

        }
      })
      
}

function returnSaveInvoice(msg){
    //window.open(msg.d,'Download');
    var resp = JSON.parse(msg.d);
    if(resp.response=="success")
    {
        //$('#sup'+resp.message).hide();
        $("#dwxml").attr("href",resp.path+resp.xmlFile).attr("download",resp.xmlFile).css('display','block');
        $("#dwpdf").attr("href",resp.path+resp.pdfFile).attr("download",resp.pdfFile).css('display','block');
        document.getElementsByName("serie")[0].value=resp.serie;
        document.getElementsByName("folio")[0].value=resp.folio;
        swal.fire("Generado", "Datos Almacenados con éxito", "success")
        .then((result) => {
            if (result.value) {
                if(validateEmail($("#email").val())){
                    Swal.fire({
                        title: 'Enviar correo',
                        text: "Quieres enviar los documentos por correo?",
                        icon: 'info',
                        showCancelButton: true,
                        confirmButtonText: 'Aceptar',
                        customClass: {
                            confirmButton: 'btn btn-brand',
                            cancelButton: 'btn btn-light'
                        },
                        cancelButtonText: 'Cancelar'
                      }).then((result) => {
                        if (result.value) {
                            sendInvMail();
                        }
                      })
                }
                    
            }
          })
  
    }
    else{
        swal.fire("Problemas", resp.message+"<br>"+resp.error, "error");
        console.log(resp.error);
    }
    KTApp.unblock('#invoice_form');

}

function saveGuide(){

    var form = $( "#guide_form" );
    form.validate({
        // define validation rules
        ignore: "",
        rules: {
            fullname: {
                required: true
            },
            recdate: {
                required: true
            },
            solicitant: {
                required: true
            },
            destinyzip: {
                required: true
            },
            fullname: {
                required: true
            },
            schedule: {
                required: true
            },
            caddress: {
                required: true
            },
            cneighborhood: {
                required: true
            },
            czipcode: {
                required: true
            },
            ccity: {
                required: true
            },
            cstate: {
                required: true
            },
            cphone: {
                required: true
            },
            ccontact: {
                required: true
            },
            name: {
                required: true
            },
            address: {
                required: true
            },
            neighborhood: {
                required: true
            },
            state: {
                required: true
            },
            city: {
                required: true
            },
            contact: {
                required: true
            },
            phone: {
                required: true
            },
            content: {
                required: true
            },
            weight: {
                required: true
            },
            width: {
                required: true
            },
            length: {
                required: true
            },
            height: {
                required: true
            },
            ccontact: {
                required: true
            },
            rfcRemitente: {
                required: true
            },
            numIdentFiscalRemitente: {
                required: true,
                maxlength: 50
            },
            rfcDestino: {
                required: true
            },
            numIdentFiscalDestino: {
                required: true,
                maxlength: 50
            },
        }
    });  

    var doReturn=false;
    if($("#cmbDeliveryType").val()==-1){
        $("#cmbDeliveryType").addClass("is-invalid"); doReturn=true;
    }
    if($("#cmbDeliveryType").val()==2 & $("#store").val()=="-1" ){
        $("#store").addClass("is-invalid"); doReturn=true;
    }
    if($("#cmbpacktype").val()==-1){
        $("#cmbpacktype").addClass("is-invalid"); doReturn=true;
    }
    if($("#cmbinsured").val()==-1){
        $("#cmbinsured").addClass("is-invalid"); doReturn=true;
    }
    if($("#cmbinsured").val()==1 & $("#invoicevalue").val()==""){
        doReturn=true;
        swal.fire("Revisar valor factura", "Debe establecer valor factura", "warning");
    }
    if ($("#fiscalRecidenceRemitente").val() == -1) {
        $("#fiscalRecidenceRemitente").addClass("is-invalid"); doReturn = true;
    }
    if ($("#fiscalRecidenceDestino").val() == -1) {
        $("#fiscalRecidenceDestino").addClass("is-invalid"); doReturn = true;
    }

    console.log($("#cansave").val());
    var cansave=true;
    if($("#cansave").val()=="0")
        { 
            doReturn=true;
            cansave=false;
        }
    
   
    if(!form.valid() | doReturn)
    {
        var alert = $('#kt_form_1_msg');
        alert.removeClass('kt--hide').show();
        if(!cansave)
            swal.fire("Revisar CP Destino", "CP Destino no tiene cobertura", "warning");
        KTUtil.scrollTo('kt_form_1_msg', -200);
        return;
    }
    
    Swal.fire({
        title: 'Generar guía',
        text: "Esta seguro que los datos son correctos, no se puede modificar una vez guardado?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Aceptar',
        customClass: {
            confirmButton: 'btn btn-brand',
            cancelButton: 'btn btn-danger'
        },
        cancelButtonText: 'Cancelar'
      }).then((result) => {
        if (result.value) {
            $("#statename").val($("#state option:selected").text());
            $("#cityname").val($("#city option:selected").text());
            console.log($("#statename").val() + $("#cityname").val());
            KTApp.block('#invoice_form', {
                overlayColor: '#000000',
                type: 'v2',
                state: 'primary',
                message: 'Guardando Guía...'
            });
            var urlMethod = "GetHint.aspx/SaveGuide";
            var jsonData = "{form: " + JSON.stringify($("#guide_form").serializeArray()) + "}";
            SendAjax(urlMethod, jsonData, returnSaveGuide); 

        }
      })
      
}

function returnSaveGuide(msg){
    //window.open(msg.d,'Download');
    var resp = JSON.parse(msg.d);
    if(resp.response=="success")
    {
        //$('#sup'+resp.message).hide();
        //$("#dwxml").attr("href",resp.path+resp.xmlFile).attr("download",resp.xmlFile).css('display','block');
        $("#dwpdf").attr("href",resp.path).attr("download",resp.pdfFile).css('display','block');
        //document.getElementsByName("serie")[0].value=resp.serie;
        document.getElementsByName("guide")[0].value=resp.guide;
        swal.fire("Generado", "Datos Almacenados con éxito", "success")
        
  
    }
    else{

        if(resp.response=="warning")
        {
            swal.fire("Atención", resp.message, "warning");
        }
        else
        {swal.fire("Problemas", resp.message+"<br>"+resp.error, "error");
        console.log(resp.error);}
    }
    KTApp.unblock('#guide_form');

}


function cancelSaleInvoice(invoiceID) {

    Swal.fire({
        title: 'Cancelar factura',
        text: "Seguro que desea cancelar?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Aceptar',
        customClass: {
            confirmButton: 'btn btn-brand',
            cancelButton: 'btn btn-danger'
        },
        cancelButtonText: 'Cancelar'
      }).then((result) => {
        if (result.value) {
            KTApp.block('#sinvrow', {
                overlayColor: '#000000',
                type: 'v2',
                state: 'primary',
                message: 'Cancelando factura...'
            });
            var urlMethod = "GetHint.aspx/CancelSaleInvoice";
            var jsonData = "{invoiceID: '"+invoiceID+"'}";
            SendAjax(urlMethod, jsonData, ReturnCancelSaleInvoice);
        }
    })
    
}
function ReturnCancelSaleInvoice(msg) {
    var listItems = "";
    var resp = JSON.parse(msg.d);
    if(resp.response=="success")
    {   
        swal.fire("Cancelada", "Factura cancelada con éxito", "success");
        getSaleInvoices();
    }
    else
    {
        swal.fire("Problemas", resp.message+"<br>"+resp.error, "error");
        console.log(resp.error); 
    }
    
    KTApp.unblock('#sinvrow');
    
}

function AddPayment(){
    
    var form = $( "#salepayment_form" );
    var doReturn=false;

    form.validate({
        // define validation rules
        ignore: "",
        rules: {
            spreference: {
                required: true
            },
            spexchange_rate: {
                required: true
            },
            sppay_total: {
                required: true
            }
        }
    });  

    if($("#spbank").val()==-1){
        $("#spbank").parent().addClass("is-invalid"); doReturn=true;
    }
    if($("#sppay_method").val()==-1){
        $("#sppay_method").parent().addClass("is-invalid"); doReturn=true;
    }
    if($("#spaccount").val()==-1){
        $("#spaccount").parent().addClass("is-invalid"); doReturn=true;
    }
    if($("#spcurrency").val()==-1){
        $("#spcurrency").parent().addClass("is-invalid"); doReturn=true;
    }
    
    if($("#sppay_total").val()==0 || $("#sppay_total").val()==""){
        swal.fire("Monto a cobrar", "Favor de ingresar el monto a cobrar", "warning");
        return;
    }

    if(!form.valid() | doReturn)
    {
        var alert = $('#kt_form_sp_msg');
        alert.removeClass('kt--hide').show();
        $('#saleinvpay_modal').scrollTop(0);
        return;
    }
    
    Swal.fire({
        title: 'Cobrar',
        text: "Los datos son correctos?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Aceptar',
        customClass: {
            confirmButton: 'btn btn-brand',
            cancelButton: 'btn btn-danger'
        },
        cancelButtonText: 'Cancelar'
      }).then((result) => {
        if (result.value) {



            KTApp.block('#saleinvpay_modal', {
                overlayColor: '#000000',
                type: 'v2',
                state: 'primary',
                message: 'Guardando Cobro...'
            });
            var table=  $("#tblsaleinvpay").DataTable();
            var arr = new Array(table.data().rows().lenght);
            var i=0;
            var requireCfdi=false;
            var genCfdi=false;
            table.rows().every( function ( rowIdx, tableLoop, rowLoop ) {
                var data = this.data();
                arr[i] = data;
                var val = arr[i][5];                     
                var initStr= val.lastIndexOf(">",val.lastIndexOf(">")-1)+1;
                var endStr = val.lastIndexOf("<");
                val = val.substr(initStr,(endStr-initStr));
                console.log(val);
                if(arr[i][11]=="PPD" & val!="0" & val!=""){
                    requireCfdi=true;
                    genCfdi=true;
                 }
                i++;
                
            });
            if (!requireCfdi)
            {
                Swal.fire({
                    title: 'Generar CFDI',
                    text: "No es necesario generar complemento de pago\nDesea generarlo de todas maneras?",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonText: 'Si',
                    customClass: {
                        confirmButton: 'btn btn-brand',
                        cancelButton: 'btn btn-danger'
                    },
                    cancelButtonText: 'No'
                  }).then((result) => {
                    if (result.value) {
                        genCfdi=true;
                    }
                    console.log(arr);
                    var urlMethod = "GetHint.aspx/AddPayment";
                    var jsonData = "{form: " + JSON.stringify($("#salepayment_form").serializeArray()) + ",docs: " + JSON.stringify(arr)+",genCP:\""+genCfdi+"\"}";
                    SendAjax(urlMethod, jsonData, returnAddPayment);
                });
            }
            else
            {
            console.log(arr);
            var urlMethod = "GetHint.aspx/AddPayment";
            var jsonData = "{form: " + JSON.stringify($("#salepayment_form").serializeArray()) + ",docs: " + JSON.stringify(arr)+",genCP:\""+genCfdi+"\"}";
            SendAjax(urlMethod, jsonData, returnAddPayment); 
            }

        }
      })
}

function returnAddPayment(msg){
    var resp = JSON.parse(msg.d);
    if(resp.response=="success")
    {
        $("#dwpxml").attr("href",resp.path+resp.xmlFile).attr("download",resp.xmlFile).css('display','block');
        $("#dwppdf").attr("href",resp.path+resp.pdfFile).attr("download",resp.pdfFile).css('display','block');
        document.getElementsByName("spserie")[0].value=resp.serie;
        document.getElementsByName("spfolio")[0].value=resp.folio;
        swal.fire("Generado", "Datos Almacenados con éxito", "success")
        .then((result) => {
            if (result.value) { 
                if(validateEmail($("#spemail").val())){
                    Swal.fire({
                        title: 'Enviar correo',
                        text: "Quieres enviar los documentos por correo?",
                        icon: 'info',
                        showCancelButton: true,
                        confirmButtonText: 'Aceptar',
                        customClass: {
                            confirmButton: 'btn btn-brand',
                            cancelButton: 'btn btn-light'
                        },
                        cancelButtonText: 'Cancelar'
                      }).then((result) => {
                        if (result.value) {
                            sendSPMail();
                        }
                      })
                }    
            }
          })
  
    }
    else{
        swal.fire("Problemas", resp.message+"<br>"+resp.error, "error");
        console.log(resp.error);
    }
    KTApp.unblock('#saleinvpay_modal');
}

function validateEmail(eml){

    var valid = true;
    var emails = eml.replace(/\s/g,'').split(",");
    var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,}$/i;

    for (var i = 0; i < emails.length; i++) {
        if( emails[i] == "" || ! testEmail.test(emails[i])){
            valid = false;
        }
    }

   return valid;
}

function sendInvMail(){

    var eml=$("#email");
    var serie=document.getElementsByName("serie")[0].value;
    var folio=document.getElementsByName("folio")[0].value;

    console.log(serie+" "+ folio);

    if(serie=="" || folio=="")
    {
        swal.fire("Revisar","No esta cargada una factura para enviar", "warning");
        return;
    }

    var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,}$/i;
        if (validateEmail(eml.val())) 
        {
            eml.removeClass("is-invalid");
            console.log("valid");
        }
        else 
        {
            eml.addClass("is-invalid");
            console.log("invalid");
            return;
        } 
  
    KTApp.block('#invoice_form', {
        overlayColor: '#000000',
        type: 'v2',
        state: 'primary',
        message: 'Enviando Correo...'
    });
    var urlMethod = "GetHint.aspx/SendInvEmail";
    var jsonData = "{serie: \""+serie+"\","+
    "folio: \""+folio+"\","+
    "email: \""+document.getElementsByName("email")[0].value+"\","+
    "customer: \""+document.getElementsByName("fullname")[0].value+"\","+
    "total: \""+document.getElementsByName("total")[0].value+"\","+
    "comments: \""+document.getElementsByName("comments")[0].value+"\""+
    "}";
    SendAjax(urlMethod, jsonData, returnSendInvMail); 
}


function returnSendInvMail(msg){
    //window.open(msg.d,'Download');
    var resp = JSON.parse(msg.d);
    if(resp.response=="success")
    {
        
        swal.fire("Enviado", "Correo enviado con éxito", "success");
    }
    else{
        swal.fire("Problemas", resp.message+"<br>"+resp.error, "error");
        console.log(resp.error);
    }
    KTApp.unblock('#invoice_form');

}

function sendSPMail(){

    var eml=$("#spemail");
    var serie=document.getElementsByName("spserie")[0].value;
    var folio=document.getElementsByName("spfolio")[0].value;

    console.log(serie+" "+ folio);

    if(serie=="" || folio=="")
    {
        swal.fire("Revisar","No esta cargado un recibo de pago para enviar", "warning");
        return;
    }

    var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,}$/i;
        if (validateEmail(eml.val())) 
        {
            eml.removeClass("is-invalid");
            console.log("valid");
        }
        else 
        {
            eml.addClass("is-invalid");
            console.log("invalid");
            return;
        } 
  
    KTApp.block('#saleinvpay_modal', {
        overlayColor: '#000000',
        type: 'v2',
        state: 'primary',
        message: 'Enviando Correo...'
    });
    var urlMethod = "GetHint.aspx/SendSPEmail";
    var jsonData = "{serie: \""+serie+"\","+
    "folio: \""+folio+"\","+
    "email: \""+document.getElementsByName("spemail")[0].value+"\","+
    "customer: \""+document.getElementById("spcustname").innerText+"\","+
    "total: \""+document.getElementsByName("sppay_total")[0].value+"\","+
    "comments: \""+document.getElementsByName("spreference")[0].value+"\""+
    "}";
    SendAjax(urlMethod, jsonData, returnSendSPMail); 
}


function returnSendSPMail(msg){
    //window.open(msg.d,'Download');
    var resp = JSON.parse(msg.d);
    if(resp.response=="success")
    {
        
        swal.fire("Enviado", "Correo enviado con éxito", "success");
    }
    else{
        swal.fire("Problemas", resp.message+"<br>"+resp.error, "error");
        console.log(resp.error);
    }
    KTApp.unblock('#saleinvpay_modal');

}

function getPOLines()
{
    var urlMethod = "GetHint.aspx/GetPOLines";
    var jsonData = "{form: " + JSON.stringify($("#po_form").serializeArray()) + "}";
    SendAjax(urlMethod, jsonData, ReturnPOLines); 
}


function ReturnPOLines(msg) {
    
    var ms = JSON.parse(msg.d);
    
         var table = $('#tbl_pch_order_lines').DataTable();
         table.clear();
         var ms = JSON.parse(msg.d);
         //console.log(ms);
         table.rows.add(ms);
         table.draw();
         //$("#btnUpldXml").html("Subir XML");

}

function getInvStatus()
{
    var urlMethod = "GetHint.aspx/GetInvStatus";
    var jsonData = "{form: " + JSON.stringify($("#forma_reporte").serializeArray()) + "}";
    SendAjax(urlMethod, jsonData, ReturnInvStatus);
}

function getInvDetail(pID)
{
    $("#pID").val(pID);
    var urlMethod = "GetHint.aspx/GetInvDetail";
    var jsonData = "{form: " + JSON.stringify($("#forma_reporte").serializeArray()) + "}";
    SendAjax(urlMethod, jsonData, ReturnInvDetail);
}

function ReturnInvDetail(msg) {

    console.log(msg.d);
    var resp = JSON.parse(msg.d);
    $("#uuidDet").html(resp.uuid);
    $("#supNameDet").html(resp.suppliername);
    $("#typeDet").html(resp.type);
    $("#poDet").html(resp.po);
    getChat();
}

function getInvHistory()
{
    var urlMethod = "GetHint.aspx/GetInvHistory";
    var jsonData = "{form: " + JSON.stringify($("#forma_reporte").serializeArray()) + "}";
    SendAjax(urlMethod, jsonData, ReturnInvHistory);
}

function ReturnInvHistory(msg) {

    console.log(msg.d);
    //var resp = JSON.parse(msg.d);
    $("#lstHistory").empty();
    $("#lstHistory").append(msg.d);

}

function GetNotifications(){

    var urlMethod = "GetHint.aspx/GetNotifications";
    var jsonData = "{form: " + JSON.stringify($("#forma_reporte").serializeArray()) + "}";
    SendAjax(urlMethod, jsonData, ReturnNotifications);
}

function ReturnNotifications(msg) {

    //console.log(msg.d);
    var resp = msg.d.split("|")
    $("#lstNotifications").append(resp[0]);
    $("#notifqty").html(resp[1]);
    if(+resp[1]==0)
        $('#notifpulsate').removeClass("kt-pulse__ring");
        
}

function DeleteNotification(notifid){
    var urlMethod = "GetHint.aspx/DeleteNotifications";
    var jsonData = "{notifid: "+notifid+" }";
    SendAjax(urlMethod, jsonData, ReturnDeleteNotif);
}

function ReturnDeleteNotif(msg){
    var resp = JSON.parse(msg.d);
    if(resp.response=="success")
    {
        $('#notif'+resp.message).hide();
        var qty=$('#notifqty').html();
        if(+qty -1 ==0)
            $('#notifpulsate').removeClass("kt-pulse__ring");
        $('#notifqty').html(+qty - 1);
    }
    else{
        console.log(resp.error);
    }
}

function getChat()
{
    var urlMethod = "GetHint.aspx/GetChat";
    var jsonData = "{form: " + JSON.stringify($("#forma_reporte").serializeArray()) + "}";
    SendAjax(urlMethod, jsonData, ReturnChat);
}

function ReturnChat(msg)
{
    var messagesEl = $('.kt-chat__messages');
    messagesEl.empty();
    messagesEl.append(msg.d);
    
    var scrollEl = KTUtil.find(KTUtil.getByID('kt_chat_modal'), '.kt-scroll');
    console.log(JSON.stringify(scrollEl));
			scrollEl.scrollTop = parseInt(messagesEl.height()+30);

			var ps;
			if (ps = KTUtil.data(scrollEl).get('ps')) {
                console.log(JSON.stringify(ps));
				ps.update();
			}
    
    //alert(messagesEl.height());
    // scrollEl.scrollTop = parseInt(messagesEl.height());
    // var ps;
    //         ps = $('#chatscroll .ps'); 
    //         console.log(JSON.stringify(ps));
	// 			//ps.update();
			
}

function ReturnInvStatus(msg) {

    $("#kt_chart_revenue_change").empty();
    
    var info = msg.d.split("|");
    var res = info[0].split(",");

    Morris.Donut({
        element: 'kt_chart_revenue_change',
        data: [{

                label: "OC Pendientes",
                value: res[0]
            },
            {
                label: "Pendientes",
                value: res[2]
            },
            {
                label: "Validadas",
                value: res[4]
            },
            {
                label: "Programadas",
                value: res[6]
            }
            ,
            {
                label: "Pagadas",
                value: res[8]
            }
        ],
        colors: [
            KTApp.getStateColor('primary'),
            KTApp.getStateColor('danger'),
            KTApp.getStateColor('warning'),
            KTApp.getStateColor('brand'),
            KTApp.getStateColor('success')
        ],
    });

    
    $("#stat_oc_pendent").html(res[0] + " OC Pendientes " + "("+formatter.format(res[1])+")");
    $("#stat_pendent").html(res[2] + " Pendientes "+ "("+formatter.format(res[3])+")");
    $("#stat_valid").html(res[4] + " Validadas "+ "("+formatter.format(res[5])+")");
    $("#stat_programed").html(res[6] + " Programadas "+ "("+formatter.format(res[7])+")");
    $("#stat_paid").html(res[8] + " Pagadas "+ "("+formatter.format(res[9])+")");
    $("#stat_total").html((+res[0]+ +res[2]+ +res[4]+ +res[6]+ +res[8]) + " Total "+ "("+formatter.format(+res[1] + +res[3]+ +res[5]+ +res[7]+ +res[9])+")");

    $("#topten").html(info[1]);
    console.log(info[1]);
    $("#custguides").html(info[1]);

}

const formatter = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',
    minimumFractionDigits: 2
  })

var redraw=false;

function getQuotes(redrw) {
    var urlMethod = "GetHint.aspx/GetQuotes";
    var jsonData = "{form: " + JSON.stringify($("#forma_reporte").serializeArray()) + "}";
    if(redrw)
        redraw=true;
    else
        redraw=false;
    SendAjax(urlMethod, jsonData, ReturnQuotes);

}

function getDSource() {
    var table = $('#kt_datatable_latest_orders').KTDatatable();
    //table.data.source = ms;
    //table.data().source()= ms;
    //table.extractTable();
    //console.log(table.data.source);
}

function ReturnQuotes(msg) {
    
    var ms = JSON.parse(msg.d);
    //var table = $('#kt_datatable_latest_orders').KTDatatable();
    //table.rows.add = ms;
    //table.options.data.source= ms;

    //LatestOrders(ms);

    //table.dataSource = ms;
    //table.dataSet= ms;
    //table.table.data.source=ms;
    //console.log(table.options.data.source);
         //console.log(ms);
         //table.reload();
        // table.redraw();
         //table.sort('name', 'desc');

         var table = $('#tablasesiones').DataTable();
         table.clear();
         var ms = JSON.parse(msg.d);
         //console.log(ms);
         table.rows.add(ms);
         if(redraw)
            table.draw('');
         else
            table.draw('full-hold');

         getInvStatus();

}

function getSaleInvoices() {
    var urlMethod = "GetHint.aspx/GetSaleInvoices";
    var jsonData = "{form: " + JSON.stringify($("#formsinv").serializeArray()) + "}";
    SendAjax(urlMethod, jsonData, ReturnSaleInvoices);


}
function ReturnSaleInvoices(msg) {
    
    var ms = JSON.parse(msg.d);
    
         var table = $('#saleinvoicestable').DataTable();
         table.clear();
         var ms = JSON.parse(msg.d);
         //console.log(ms);
         table.rows.add(ms);
         table.draw();
         //Not necesary to hold, because always want to fully redraw
         //table.draw('full-hold');
         

}
function getInvoiceLines(invID)
{
    var urlMethod = "GetHint.aspx/GetInvoiceLines";
    var jsonData = "{invoiceID: " + invID + "}";
    SendAjax(urlMethod, jsonData, ReturnInvoiceLines); 
}


function ReturnInvoiceLines(msg) {
    
    var ms = JSON.parse(msg.d);
    
         var table = $('#tbl_invoice_lines').DataTable();
         table.clear();
         var ms = JSON.parse(msg.d);
         //console.log(ms);
         table.rows.add(ms);
         table.draw();
         //$("#btnUpldXml").html("Subir XML");

}

function getSalePayDocs(custID)
{
    var urlMethod = "GetHint.aspx/GetSalePaymentDocs";
    var jsonData = "{custID: " + custID + "}";
    SendAjax(urlMethod, jsonData, ReturnGetSalePayDocs); 
}

function ReturnGetSalePayDocs(msg) {
    
    var ms = JSON.parse(msg.d);
    
         var table = $('#tblsaleinvpay').DataTable();
         table.clear();
         var ms = JSON.parse(msg.d);
         console.log(ms);
         table.rows.add(ms.salePayDocs);
         table.draw();
         table.columns(7).visible(false);
         table.columns(8).visible(false);

         $("#spcustid").html(ms.custID);
         $("#spcustname").html(ms.custName);
         $("#spemail").val(ms.custEmail);
         $("#spcustomerid").val(ms.custID);

         getCatalog("GetHint.aspx/GetCatalog","spcurrency","currency","currency_id","code","name","1","","ORDER BY currency_id");
         getCatalog("GetHint.aspx/GetCatalog","sppay_method","payment_method","code","name","","","","ORDER BY payment_method_id","");
         getCatalog("GetHint.aspx/GetCatalog","spbank","banks","bank_id","name","","","","ORDER BY bank_id","");


}


function getSalePayments() {
    var urlMethod = "GetHint.aspx/GetSalePayments";
    var jsonData = "{form: " + JSON.stringify($("#formsp").serializeArray()) + "}";
    SendAjax(urlMethod, jsonData, ReturnSalePayments);


}
function ReturnSalePayments(msg) {
    
    var ms = JSON.parse(msg.d);
    
         var table = $('#salepaymentstable').DataTable();
         table.clear();
         var ms = JSON.parse(msg.d);
         //console.log(ms);
         table.rows.add(ms.recact);
         table.draw();
         //Not necesary to hold, because always want to fully redraw
         //table.draw('full-hold');
         
         var table2 = $('#tblsalepaymentstotal').DataTable();
         table2.clear();
         //console.log(ms);
         table2.rows.add(ms.totalrecact);
         table2.draw();

}

function loadInvoice(invID)
{
    var urlMethod = "GetHint.aspx/GetSaleInvoice";
    var jsonData = "{invoiceID: " + invID + "}";
    SendAjax(urlMethod, jsonData, ReturnLoadInvoice); 
}

function ReturnLoadInvoice(msg){

    var si=JSON.parse(msg.d);
    //console.log(msg.d);
    var ms=si.saleInvoice;    
    //console.log(ms.city_id);
    getCatalog("GetHint.aspx/GetCatalog","currency","currency","currency_id","code","name",ms.currency_id,"","ORDER BY currency_id","");
    getCatalog("GetHint.aspx/GetCatalog","state","states","state_id","state_name","state_code",ms.state_id,"country_id=1","ORDER BY state_name",ms.city_id);
    getCatalog("GetHint.aspx/GetCatalog","pay_method","payment_method","code","name","",ms.pay_method,"","ORDER BY payment_method_id","");
    getCatalog("GetHint.aspx/GetCatalog","pay_method_cond","pay_method","code","name","",ms.pay_method_cond,"","ORDER BY pay_method_id","");
    getCatalog("GetHint.aspx/GetCatalog","cfdi_usage","cfdi_usage","code","name","",ms.cfdi_usage_id,"","ORDER BY cfdi_usage_id","");
    document.getElementsByName("serie")[0].value=ms.serie;
    document.getElementsByName("folio")[0].value=ms.code;
    document.getElementsByName("fullname")[0].value=ms.name;
    document.getElementsByName("rfc")[0].value=ms.legal_code;
    document.getElementsByName("address")[0].value=ms.street;
    document.getElementsByName("no_ext")[0].value=ms.no_ext;
    document.getElementsByName("no_int")[0].value=ms.no_int;
    document.getElementsByName("neighborhood")[0].value=ms.neighborhood;
    document.getElementsByName("postal_code")[0].value=ms.postal_code;
    document.getElementsByName("email")[0].value=ms.emails;  
    document.getElementsByName("customerid")[0].value=ms.customer_id;  
    document.getElementsByName("comments")[0].value=ms.description; 
    var count=0;
    si.saleInvoiceLines.forEach(function(item) {
        //console.log(item);
        if(count>0)
            $("#btnlnrepeater").click();
        $("[name='["+count+"][lncode]']").val(item[0]);
        $("[name='["+count+"][lnqty]']").val(item[1]);
        $("[name='["+count+"][lnproduct]']").val(item[2]);
        $("[name='["+count+"][lnprice]']").val(item[3]);
        $("[name='["+count+"][lntotal]']").val(item[4]);
        $("[name='["+count+"][lnunit]']").val(item[5]);
        $("[name='["+count+"][lnsatcode]']").val(item[6]);
        $("[name='["+count+"][lnsatunit]']").val(item[7]);
        if(item[8]=="")
            $("[name='["+count+"][lntax]']").val(16);
        else
            $("[name='["+count+"][lntax]']").val(item[8]);
        if(item[9]=="")
            $("[name='["+count+"][lndiscount]']").val(0);
        else
            $("[name='["+count+"][lndiscount]']").val(item[9]);
        count++;
      }); 
    $("#makeinvoice").css('display','block');
    $("#sinvrow").css('display','none');
    $("#dwxml").attr("href",si.path+si.xmlFile).attr("download",si.xmlFile).css('display','block');
    $("#dwpdf").attr("href",si.path+si.pdfFile).attr("download",si.pdfFile).css('display','block');
    $(".ln-leave").trigger('blur');
}

function getSaleInvCatalogs()
{
    var urlMethod = "GetHint.aspx/GetSaleInvCatalogs";
    var jsonData = "{}";
    SendAjax(urlMethod, jsonData, ReturnSaleInvCatalogs); 
}

function ReturnSaleInvCatalogs(msg) {
    
    //console.log(msg.d);
    var ms = JSON.parse(msg.d);
    
    ms.currency.currencies.forEach(function(item) {
        $('#currency').append($('<option>', {value: item.currency_id, text: item.code}));
      }); 
      $("#currency").val(1);

}

function getRanges(redrw) {
    var urlMethod = "GetHint.aspx/GetRanges";
    var jsonData = "{form: " + JSON.stringify($("#auth_form").serializeArray()) + "}";
    if(redrw)
        redraw=true;
    else
        redraw=false;
    SendAjax(urlMethod, jsonData, ReturnRanges);


}
function ReturnRanges(msg) {
    
    var ms = JSON.parse(msg.d);
    
         var table = $('#tblguideranges').DataTable();
         table.clear();
         var ms = JSON.parse(msg.d);
         console.log(ms);
         table.rows.add(ms);
         if(redraw)
            table.draw('');
         else
            table.draw('full-hold');
         //Not necesary to hold, because always want to fully redraw
         //table.draw('full-hold');
         

}


function saveRange(){

    var form = $( "#auth_form" );
    form.validate({
        // define validation rules
        ignore: "",
        rules: {
            toauthorize: {
                required: true
            }
        }
    });  

    if($("#customer").val()==-1){
        $("#customer").parent().addClass("is-invalid"); doReturn=true;
    }

    var doReturn=false;
  
    if(!form.valid() | doReturn)
    {
        var alert = $('#kt_form_1_msg');
        alert.removeClass('kt--hide').show();
        KTUtil.scrollTo('kt_form_1_msg', -200);
        return;
    }
    
    Swal.fire({
        title: 'Autorizar',
        text: "Los datos son correctos?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Aceptar',
        customClass: {
            confirmButton: 'btn btn-brand',
            cancelButton: 'btn btn-danger'
        },
        cancelButtonText: 'Cancelar'
      }).then((result) => {
        if (result.value) {
            KTApp.block('#auth_form', {
                overlayColor: '#000000',
                type: 'v2',
                state: 'primary',
                message: 'Guardando...'
            });
            var urlMethod = "GetHint.aspx/SaveRange";
            var jsonData = "{form: " + JSON.stringify($("#auth_form").serializeArray()) + "}";
            SendAjax(urlMethod, jsonData, returnSaveRange); 

        }
      })
      
}

function returnSaveRange(msg){
    //window.open(msg.d,'Download');
    var resp = JSON.parse(msg.d);
    if(resp.response=="success")
    {
        getRanges(true);
        document.getElementsByName("range")[0].value=resp.range;
        swal.fire("Generado", "Datos Almacenados con éxito", "success")
    }
    else{
        if(resp.response=="warning")
        {
            swal.fire("Atención", resp.message, "warning");
            console.log(resp.error);
        }
        else
        {
        swal.fire("Problemas", resp.message+"<br>"+resp.error, "error");
        console.log(resp.error);
        }
    }
    KTApp.unblock('#auth_form');

}

function getLastGuide(id,auth)
{
    var urlMethod = "GetHint.aspx/GetLastGuide";
    var jsonData = "{customerID: " + id + ",auth:"+auth+"}";
    SendAjax(urlMethod, jsonData, ReturnLastGuide); 
}

function ReturnLastGuide(msg) {
    var ms = JSON.parse(msg.d);
    console.log(ms.lastused);
    if (ms.auth == 'True') {
        $("#guidesleft").html(ms.available);
        if (+ms.available < 6)
            swal.fire("Atención", "Quedan sólo " + ms.available + " guías disponibles, favor de ponerse en contacto con un representante para solicitar más guías", "warning");
        document.getElementsByName("fullname")[0].value = ms.contact;
        document.getElementsByName("schedule")[0].value = ms.schedule;
        document.getElementsByName("caddress")[0].value = ms.address;
        document.getElementsByName("cneighborhood")[0].value = ms.neighborhood;
        document.getElementsByName("czipcode")[0].value = ms.zipcode;
        document.getElementsByName("creference")[0].value = ms.reference;
        document.getElementsByName("ccity")[0].value = ms.city.toUpperCase();
        document.getElementsByName("cstate")[0].value = ms.state.toUpperCase();
        document.getElementsByName("cphone")[0].value = ms.phone;
        document.getElementsByName("ccontact")[0].value = ms.contact;
        document.getElementsByName("rfcRemitente")[0].value = ms.rfcRemitente;


    }
    else {
        document.getElementsByName("range")[0].value = ms.range;
        document.getElementsByName("lastused")[0].value = ms.lastused;
        document.getElementsByName("custname")[0].value = ms.customer;

    }
}

function checkZip()
{
    if($("#destinyzip").val()!="")
    {
    var urlMethod = "GetHint.aspx/CheckZip";
    var jsonData = "{zip: '" + $("#destinyzip").val() + "'}";
    SendAjax(urlMethod, jsonData, ReturnCheckZip); 
    }
}

function ReturnCheckZip(msg) {
    var ms=JSON.parse(msg.d);
      if(ms.response=='success'){

        //getCatalog("GetHint.aspx/GetCatalog","state","states","state_id","state_name","state_code",ms.state_id,"country_id=1","ORDER BY state_name",ms.city_id);
        document.getElementsByName("state")[0].value=ms.state;
        document.getElementsByName("city")[0].value=ms.city;
        document.getElementsByName("neighborhood")[0].value=ms.neighborhood;
        document.getElementsByName("cansave")[0].value=1;
      }
      else if(ms.response=='warning')
      {
        swal.fire("Atención", ms.message, "warning");
        document.getElementsByName("cansave")[0].value=0;
      }
      else{
        swal.fire("Problemas", ms.message+"<br>"+ms.error, "error");
        document.getElementsByName("cansave")[0].value=0;
      }
}


function getOcurreInfo(id)
{
    
    var urlMethod = "GetHint.aspx/GetOcurreInfo";
    var jsonData = "{id: " + id + "}";
    SendAjax(urlMethod, jsonData, ReturnGetOcurreInfo); 
    
}

function ReturnGetOcurreInfo(msg) {
    var ms=JSON.parse(msg.d);
      if(ms.response=='success'){

       document.getElementsByName("state")[0].value=ms.state;
        document.getElementsByName("city")[0].value=ms.city;
        document.getElementsByName("neighborhood")[0].value=ms.neighborhood;
        document.getElementsByName("address")[0].value=ms.address;
        document.getElementsByName("phone")[0].value=ms.phone;
        $("#destinyzip").val(ms.zipcode);
        document.getElementsByName("cansave")[0].value=1;
        
      }
      else if(ms.response=='warning')
      {
        swal.fire("Atención", ms.message, "warning");
      }
      else{
        swal.fire("Problemas", ms.message+"<br>"+ms.error, "error");
      }
}


function ReturnCustomer(msg) {
    var ms=JSON.parse(msg.d);
    console.log(msg.d);
    $("#state").val(ms.state);
    $("#state").trigger("change",[ms.city]);
    $("[name='customerid']").val(ms.customer_id);
    document.getElementsByName("fullname")[0].value=ms.name;
    document.getElementsByName("rfc")[0].value=ms.rfc;
    document.getElementsByName("address")[0].value=ms.street;
    document.getElementsByName("no_ext")[0].value=ms.no_ext;
    document.getElementsByName("no_int")[0].value=ms.no_int;
    document.getElementsByName("neighborhood")[0].value=ms.neighborhood;
    //getCatalog("GetHint.aspx/GetCatalog","city","cities","city_id","city_name","",ms.city,"state_id="+ms.state+"","ORDER BY city_name"); 
    //$("#state").trigger("change",function(){document.getElementsByName("city")[0].value=ms.city_name;});
    document.getElementsByName("postal_code")[0].value=ms.postal_code;
    document.getElementsByName("email")[0].value=ms.emails;
    $("[name='pay_method']").val(ms.pay_method).trigger('change');
    $("[name='pay_method_cond']").val("PPD").trigger('change');//ms.pay_method_cond;
    $("[name='cfdi_usage']").val(ms.cfdi_usage).trigger('change');   

    //$("[name='pay_method_cond']").trigger('change');

    $("#btncustclose").click();
}

function getDeliveryInfo(){

    var store=document.getElementById("store");
    var destzip= document.getElementById("destinyzip");
    if($("#cmbDeliveryType").val()==1)
       {
        $("#store").val("-1");
        destzip.removeAttribute('readonly');
        store.setAttribute("disabled", 'disabled');
       } 
       else if($("#cmbDeliveryType").val()==2)
       {
        $("#store").empty();
        store.removeAttribute('disabled');
        getCatalog("GetHint.aspx/GetCatalog","store","ocurre_stores","ocurre_store_id","name","","","","ORDER BY name");
        destzip.setAttribute("readonly", 'readonly');
       }
}


function getCustomers()
{
    var urlMethod = "GetHint.aspx/GetCustomers";
    var jsonData = "{}";
    SendAjax(urlMethod, jsonData, ReturnCustomers); 
}

function ReturnCustomers(msg) {
    
    var ms = JSON.parse(msg.d);
         var table = $('#tbl_customers').DataTable();
         table.clear();
         var ms = JSON.parse(msg.d);
         table.rows.add(ms);
         table.columns.adjust().draw();

}


function getSubCustomer(code)
{
    var urlMethod = "GetHint.aspx/GetSubCustomer";
    var jsonData = "{code: \"" + code + "\"}";
    SendAjax(urlMethod, jsonData, ReturnSubCustomer); 
}

function ReturnSubCustomer(msg) {
    var ms=JSON.parse(msg.d);
    console.log(msg.d);
    if(ms.response=="success")
    {
    document.getElementsByName("state")[0].value=ms.state;
    document.getElementsByName("city")[0].value=ms.city;
    document.getElementsByName("name")[0].value=ms.name;
    document.getElementsByName("shortname")[0].value=ms.code;
    document.getElementsByName("address")[0].value=ms.address;
    document.getElementsByName("neighborhood")[0].value=ms.neighborhood;
    document.getElementsByName("destinyzip")[0].value=ms.postal_code;
    document.getElementsByName("contact")[0].value=ms.contact;
    document.getElementsByName("phone")[0].value=ms.phone;
    document.getElementsByName("cansave")[0].value=1;
    
    }

    $("#btncustclose").click();
}


function getCustomer(id)
{
    var urlMethod = "GetHint.aspx/GetCustomer";
    var jsonData = "{customerID: " + id + "}";
    SendAjax(urlMethod, jsonData, ReturnCustomer); 
}

function ReturnCustomer(msg) {
    var ms=JSON.parse(msg.d);
    console.log(msg.d);
    $("#state").val(ms.state);
    $("#state").trigger("change",[ms.city]);
    $("[name='customerid']").val(ms.customer_id);
    document.getElementsByName("fullname")[0].value=ms.name;
    document.getElementsByName("rfc")[0].value=ms.rfc;
    document.getElementsByName("address")[0].value=ms.street;
    document.getElementsByName("no_ext")[0].value=ms.no_ext;
    document.getElementsByName("no_int")[0].value=ms.no_int;
    document.getElementsByName("neighborhood")[0].value=ms.neighborhood;
    //getCatalog("GetHint.aspx/GetCatalog","city","cities","city_id","city_name","",ms.city,"state_id="+ms.state+"","ORDER BY city_name"); 
    //$("#state").trigger("change",function(){document.getElementsByName("city")[0].value=ms.city_name;});
    document.getElementsByName("postal_code")[0].value=ms.postal_code;
    document.getElementsByName("email")[0].value=ms.emails;
    $("[name='pay_method']").val(ms.pay_method).trigger('change');
    $("[name='pay_method_cond']").val("PPD").trigger('change');//ms.pay_method_cond;
    $("[name='cfdi_usage']").val(ms.cfdi_usage).trigger('change');   

    //$("[name='pay_method_cond']").trigger('change');

    $("#btncustclose").click();
}

function getReceivableAccounts() {
    var urlMethod = "GetHint.aspx/GetReceivableAccounts";
    var jsonData = "{form: " + JSON.stringify($("#frmcxc").serializeArray()) + "}";
    SendAjax(urlMethod, jsonData, ReturnReceivableAccounts);


}
function ReturnReceivableAccounts(msg) {
    
    var ms = JSON.parse(msg.d);
    
         var table = $('#tblreceivableaccounts').DataTable();
         table.clear();
         var ms = JSON.parse(msg.d);
         console.log(ms);
         table.rows.add(ms.recact);
         table.draw();
         //Not necesary to hold, because always want to fully redraw
         //table.draw('full-hold');

         var table2 = $('#tblreceivableaccountstotal').DataTable();
         table2.clear();
         //console.log(ms);
         table2.rows.add(ms.totalrecact);
         table2.draw();
         

}

function getCatalog(urlMethod,control,table,id,val,val2,defaultValue,condition,order,extraparam)
{
     var jsonData = "{control:'"+control+"',defaultValue:'"+defaultValue+"',table:'"+table+"',id:'"+id+"',val:'"+val+"',val2:'"+val2+"',condition:'"+condition+"',order:'"+order+"',extraparam:'"+extraparam+"'}";
     SendAjax(urlMethod,jsonData,returnCatalog);
}
function returnCatalog(msg){
    var ms = JSON.parse(msg.d);
    ms.catalog.forEach(function(item) {
    //$('#'+ms.control).append($('<option>', {value: item.id, text: item.name}));
    var option = new Option(item.name,item.id);
    option.setAttribute('data-extra', item.data);
    $('#'+ms.control).append(option);
    }); 
    if(ms.defaultValue!="")
        $("#"+ms.control).val(ms.defaultValue);
        if(ms.control=="state")
        {
            if(ms.extraparam!="")
                $("#state").trigger("change",[ms.extraparam]);
            else
                $("#state").trigger("change",['980']); 
        }
        if(ms.error!="No Exception")
            console.log(ms.error);
}

//Ajax
function SendAjax(urlMethod, jsonData, returnFunction) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: urlMethod,
        data: jsonData,
        dataType: "json",
        success: function (msg) {
            if (msg != null) {
                returnFunction(msg);
            }
        },
        error: function (xhr, status, error) {
            // Boil the ASP.NET AJAX error down to JSON.
            var err = eval("(" + xhr.responseText + ")");
            alert(err.Message);
        },
        complete: function (){
        }
    });
}

!function(){if(jQuery&&jQuery.fn&&jQuery.fn.select2&&jQuery.fn.select2.amd)var e=jQuery.fn.select2.amd;e.define("select2/i18n/es",[],function(){return{errorLoading:function(){return"No se pudieron cargar los resultados"},inputTooLong:function(e){var n=e.input.length-e.maximum,r="Por favor, elimine "+n+" car";return r+=1==n?"acter":"acteres"},inputTooShort:function(e){var n=e.minimum-e.input.length,r="Por favor, introduzca "+n+" car";return r+=1==n?"acter":"acteres"},loadingMore:function(){return"Cargando más resultados…"},maximumSelected:function(e){var n="Sólo puede seleccionar "+e.maximum+" elemento";return 1!=e.maximum&&(n+="s"),n},noResults:function(){return"No se encontraron resultados"},searching:function(){return"Buscando…"},removeAllItems:function(){return"Eliminar todos los elementos"}}}),e.define,e.require}();

function setCode(){
       
    var satcode= $('#kt_select2_1').val();
    var i= $('#clkControl').val();
    console.log(i);
    var init = i.indexOf('[');
    var fin = i.indexOf(']');
    i=i.substr(init+1,fin-init-1);
    var desc=$("#kt_select2_1 option:selected").html();
    document.getElementsByName("["+i+"][lnproduct]")[0].value= desc.substring(desc.lastIndexOf("-")+1);
    document.getElementsByName("["+i+"][lnsatcode]")[0].value=satcode;
    //$('#satcodemodalclose').click();
}

$('#SatCode').on('keyup', function (event) {  
    var keycode = (event.keyCode ? event.keyCode : event.which);
  if(keycode == '13')
  {
    $('#btnSatCode').focus();
    $('#btnSatCode').click();
    //alert("pressed");
  }
    

});

function setControl(e){
    $('#kt_select2_1').val(-1);
    $('#clkControl').val(e.name); 
}

function getToken(usrId) { 
    var urlMethod = "almex.aspx/GetToken";
    var jsonData = "{'userid': '" + usrId + "'}";
    SendAjax(urlMethod, jsonData, ReturnGetToken);
}

function ReturnGetToken(msg) {

   
    if (msg.d == "" || msg.d == "null" || msg.d == null)
        swal.fire("Error", "Hubo un problema al generar el Token.", "error");
    else
        //$('#frame').attr("src", "http://166.62.93.54/ProconecttWeb/pages/inicio.aspx?token=" + ms.data.Token + "");
        /*$('#frame').attr("src", "http://166.62.93.54/ProconecttWeb/Pages/Inicio.aspx?" + msg.d + "");*/
        $('#frame').attr("src", "http://proconecta.savi-mex.com/Web/Pages/Inicio.aspx?" + msg.d + "");
 
}
//Validacion Select Portosinos
$("#fiscalRecidenceRemitente").change(function () {
    //1 == México
    if ($("#fiscalRecidenceRemitente").val() == 1) {
        //Ocultamos El numero de Identificacion Fiscal
        $("#divNumFiscalRemitente").css('display', 'none');
    }
    else
        $("#divNumFiscalRemitente").css('display', 'block');
});

$("#fiscalRecidenceDestino").change(function () {
    //1 == México
    if ($("#fiscalRecidenceDestino").val() == 1) {
        //Ocultamos El numero de Identificacion Fiscal
        $("#divNumFiscalDestino").css('display', 'none');
    }
    else
        $("#divNumFiscalDestino").css('display', 'block');
});
//Validacion Select DetalleMercancia
//$("#cmbpacktype").change(function () {
//    //1 == Paquete
//    if ($("#cmbpacktype").val() == 1) {
//        //Ocultamos El numero de Identificacion Fiscal
//        $("#divDetalleMercancia").css('display', 'none');
//    }
//    else
//        $("#divDetalleMercancia").css('display', 'block');
//});

//Validacion RFC Potosinos
function ValidaRFC() {

    // patron del RFC, persona moral
    let _rfc_pattern_pm = "^(([A-ZÑ&]{3})([0-9]{2})([0][13578]|[1][02])(([0][1-9]|[12][\\d])|[3][01])([A-Z0-9]{3}))|" +
        "(([A-ZÑ&]{3})([0-9]{2})([0][13456789]|[1][012])(([0][1-9]|[12][\\d])|[3][0])([A-Z0-9]{3}))|" +
        "(([A-ZÑ&]{3})([02468][048]|[13579][26])[0][2]([0][1-9]|[12][\\d])([A-Z0-9]{3}))|" +
        "(([A-ZÑ&]{3})([0-9]{2})[0][2]([0][1-9]|[1][0-9]|[2][0-8])([A-Z0-9]{3}))$";
    // patron del RFC, persona fisica
    let _rfc_pattern_pf = "^(([A-ZÑ&]{4})([0-9]{2})([0][13578]|[1][02])(([0][1-9]|[12][\\d])|[3][01])([A-Z0-9]{3}))|" +
        "(([A-ZÑ&]{4})([0-9]{2})([0][13456789]|[1][012])(([0][1-9]|[12][\\d])|[3][0])([A-Z0-9]{3}))|" +
        "(([A-ZÑ&]{4})([02468][048]|[13579][26])[0][2]([0][1-9]|[12][\\d])([A-Z0-9]{3}))|" +
        "(([A-ZÑ&]{4})([0-9]{2})[0][2]([0][1-9]|[1][0-9]|[2][0-8])([A-Z0-9]{3}))$";

    var rfcRemitente = document.getElementById("rfcRemitente").value;
    var rfcDestino = document.getElementById("rfcDestino").value;

    if (rfcRemitente != "") {
        if (rfcRemitente.match(_rfc_pattern_pm) || rfcRemitente.match(_rfc_pattern_pf)) {
            $("#rfcRemitente").removeClass("is-invalid");
            $("#rfcValidationRemitente").addClass("is-valid-rfc");
        } else {
            $("#rfcValidationRemitente").removeClass("is-valid-rfc");
            $("#rfcRemitente").addClass("is-invalid");
        }
    }
    if (rfcDestino != "") {
        if (rfcDestino.match(_rfc_pattern_pm) || rfcDestino.match(_rfc_pattern_pf)) {
            $("#rfcDestino").removeClass("is-invalid");
            $("#rfcValidationDestino").addClass("is-valid-rfc");
        } else {
            $("#rfcValidationDestino").removeClass("is-valid-rfc");
            $("#rfcDestino").addClass("is-invalid");
        }
    }
}
// Class initialization on page load
jQuery(document).ready(function() {

    //var table = $('#kt_datatable_latest_orders').KTDatatable();

    $("#rfcRemitente").blur(function () {
        ValidaRFC();
    });

    $("#rfcDestino").blur(function () {
        ValidaRFC();
    });

    var path = window.location.href;
    var page = path.substr(path.lastIndexOf("/") + 1);


    if (page == "almex.aspx") {

        getToken(usrId);
        $("#body").addClass("kt-aside__brand-aside-toggler kt-aside__brand-aside-toggler--active kt-aside--minimize");
    }
    KTDashboard.init();

    var table = $('#tablasesiones').DataTable({
        
        "bPaginate": true,
        "bLengthChange": true,
        "bFilter": true,
        "bInfo": true,
        "bAutoWidth": false,
        "ordering": true,
        "editable": true,
        "responsive": true,
        "dom": 'Bfrtip',
        "buttons": [
            'excel'
        ]
    });

    var table = $('#saleinvoicestable').DataTable({
        
        "bPaginate": true,
        "bLengthChange": true,
        "bFilter": true,
        "bInfo": true,
        "bAutoWidth": false,
        "ordering": true,
        "editable": true,
        "responsive": true
    });

    var table = $('#tblreceivableaccounts').DataTable({
        
        "bPaginate": true,
        "bLengthChange": true,
        "bFilter": true,
        "bInfo": true,
        "bAutoWidth": false,
        "ordering": true,
        "editable": true,
        "responsive": true
        //"dom": 'Bfrtip',
        //"buttons": [
        //    'copy', 'csv', 'excel', 'pdf', 'print'
        //]

    });

    var table3 = $('#tbl_customers').DataTable({
        
        "bPaginate": true,
        "bLengthChange": true,
        "bFilter": true,
        "bInfo": true,
        "bAutoWidth": true,
        "ordering": true,
        "editable": true,
        "responsive": true
    });

    $('.kt_reset').on('click', function(e) {
        e.preventDefault();
        $('.kt-input').each(function() {
            $(this).val('');
            table.column($(this).data('col-index')).search('', false, false);
        });
        table.table().draw();
    });

    var table = $('#tbl_invoice_lines').DataTable({
        
        "bFilter": false,
        "bPaginate": false,
        "responsive": true
    });
    
    var table = $('#tbl_pch_order_lines').DataTable({
        
        "bFilter": false,
        "bPaginate": false,
        "responsive": true
    });

    var tablesip = $('#tblsaleinvpay').DataTable({
        
        "bFilter": false,
        "bPaginate": false,
        "responsive": true,
        "keys": true
    });

    var table = $('#tblreceivableaccountstotal').DataTable({
        
        "bFilter": false,
        "bPaginate": false,
        "responsive": true
    });

    var table = $('#salepaymentstable').DataTable({
        "responsive": true
    });

    var table = $('#tblsalepaymentstotal').DataTable({
        
        "bFilter": false,
        "bPaginate": false,
        "responsive": true
    });

    // Inline editing
            var oldValue = null;
            var lnEdit=0;
            tablesip.on( 'key-focus', function (e, datatable, cell ) {
           
            var input = $( 'a', cell.node() );
            console.log(input);
            input.click();
        } );

			$(document).on('click', '.editable', function(){
                oldValue = $(this).html();
                
                $(this).removeClass('editable');	// to stop from making repeated request
                
                lnEdit = $(this).attr('id');
				
				$(this).html('<input type="number" style="width:80px; text-align: right;" class="update" value="'+ oldValue +'" />');
                $(this).find('.update').focus();
                $(this).find('.update').select();

                
			});

			var newValue = null;
			$(document).on('blur', '.update', function(){
                updateVal(this);
                $(this).removeClass('update');
            });

             $(document).on('keyup', '.update', function(e){
                 if(e.keyCode== 13  )
                 {
                    console.log(e.keyCode);
                     updateVal(this);
                     $(this).removeClass('update');
                     tablesip.keys.move('down');
                 }
             });

             $(document).on('keydown', '.update', function(e){
                if(  e.keyCode == 9 || e.keyCode ==37 || e.keyCode ==38 || e.keyCode ==39 || e.keyCode ==40 )
                {
                    console.log(e.keyCode);
                    updateVal(this);
                    $(this).removeClass('update');
                }
            });
            
            function updateVal(val)
            {
                var elem    = $(val);
				newValue 	= $(val).val();
				var empId	= $(val).parent().attr('id');
				var colName	= $(val).parent().attr('name');

				if(newValue != oldValue)
				{
					
					$(elem).parent().addClass('editable');
                    
                    
                    if($("#actn").val()=="sinv"){
                        $(elem).parent().html(newValue); 
                        var lnEd = "#linetoedit"+ lnEdit.replace("ln","");
                        var unPr = "#unitaryprice"+lnEdit.replace("ln","");
                        var unitPrice = $(unPr).html();
                        unitPrice = unitPrice.replace("$","").replace(",","");
                        $(lnEd).html(formatter.format(+unitPrice * +newValue));
                        $("tr:eq(1) td:eq(7)").addClass("table-success");
                    }
                    else{
                        
                        var total=0;
                        var table=  $("#tblsaleinvpay").DataTable();
                        //table.rows().invalidate();
                        var lnEd = lnEdit.replace("docln","");
                        table.rows().every( function ( rowIdx, tableLoop, rowLoop ) {
                            var data = this.data();
                            console.log(data);
                            var val = data[5];                        
                            var initStr= val.lastIndexOf(">",val.lastIndexOf(">")-1)+1;
                            var endStr = val.lastIndexOf("<");
                            if(rowIdx==lnEd){
                                
                                var startString= val.substr(0,initStr);
                                var endString = val.substr(endStr,val.length- +endStr);
                                val = val.substr(initStr,(endStr-initStr));
                                
                                if(+data[4]<+newValue)
                                {
                                    val=data[4];
                                    val= Math.round(val*100)/100
                                    newValue = startString+val+endString;
                                    elem.parent().html(newValue);
                                }
                                else
                                {
                                    val=newValue;
                                    val= Math.round(val*100)/100
                                    newValue = startString+newValue+endString;
                                    $(elem).parent().html(newValue); 
                                }
                                data[5]=newValue;
                                table.rows().invalidate();
                            }
                            else
                                val = val.substr(initStr,(endStr-initStr));
                            //console.log(str);
                            console.log(val);
                            total+= +val;
                            total= Math.round(total*100)/100
                        } );
                        $("#sppay_total").val(total);
                        console.log(table.data());
                    }
                    
					
				}
				else
				{
					$(elem).parent().addClass('editable');
					$(val).parent().html(newValue);
				}
            }

    //console.log(table.options.data.source);

    $('#kt_datepicker').datepicker({
        todayHighlight: true,
        format: 'dd/mm/yyyy',
        orientation: 'bottom',
        templates: {
            leftArrow: '<i class="la la-angle-left"></i>',
            rightArrow: '<i class="la la-angle-right"></i>',
        },
    });

    $('#kt_si_datepicker').datepicker({
        todayHighlight: true,
        format: 'dd/mm/yyyy',
        orientation: 'bottom',
        templates: {
            leftArrow: '<i class="la la-angle-left"></i>',
            rightArrow: '<i class="la la-angle-right"></i>',
        },
    });

    $('#kt_recact_datepicker').datepicker({
        todayHighlight: true,
        format: 'dd/mm/yyyy',      
        orientation: 'bottom',
        templates: {
            leftArrow: '<i class="la la-angle-left"></i>',
            rightArrow: '<i class="la la-angle-right"></i>',
        },
    });
    $('#kt_sp_datepicker').datepicker({
        todayHighlight: true,
        format: 'dd/mm/yyyy',      
        orientation: 'bottom',
        templates: {
            leftArrow: '<i class="la la-angle-left"></i>',
            rightArrow: '<i class="la la-angle-right"></i>',
        },
    });
    $('#SatCode').on('shown.bs.modal', function () {
        $('#kt_select2_1').select2({
            placeholder: "Seleccione",
            minimumInputLength: 4,
            selectOnClose: true,
            dropdownParent: $('#SatCode'),
            language: "es",
            ajax: {
                url: 'GetHint.aspx/GetSatCodes',
                dataType: 'json',
                data: function(params) {
                    return {
                        search: params.term, // search term
                    };
                },
                processResults: function(data, params) {
                    console.log(data);
                    return {
                        results: data.items
                    };
                }
            }

        }).focus();

        
    });


    $('.dropdownmenu').on('click', function(event) {
        event.stopPropagation();
      });

    $('.select2').select2({
        placeholder: "Seleccione",
        selectOnClose: true,
        language: "es"
    });  

    $('#saleinvpay_modal').on('shown.bs.modal', function () {
        $('.select2').select2({
            placeholder: "Seleccione",
            selectOnClose: true,
            language: "es"
        });

        $("#tblsaleinvpay").DataTable()
        .columns.adjust()
        .responsive.recalc();
        
    });

  
    $(document).on('focus', '.select2', function() {
        $(this).siblings('select').select2('open');
    });

    $(document).on('change', '.select2', function() {
        //alert($(this).val());
        if($(this).val()!=-1)
            $(this).parent().removeClass("is-invalid");
        else
            $(this).parent().addClass("is-invalid");;        
    });

    $(document).on('change', '.fieldvalidate', function() {
        //alert($(this).val());

        if (validateEmail($(this).val())) 
            $(this).removeClass("is-invalid");
        else 
            $(this).addClass("is-invalid");
        
              
    });
 
    $("#state").on('change',function(event,param){
        var selstate = $(this). children("option:selected"). val();
        $("#city").children().remove().end();
        var defCity="980";
        //console.log(param);
        if(param!=undefined)
            defCity=param;
        getCatalog("GetHint.aspx/GetCatalog","city","cities","city_id","city_name","",defCity,"state_id="+selstate+"","ORDER BY city_name",""); 
        });
    
    $("#customer").on('change',function(event,param){
        var selcust = $(this). children("option:selected"). val();
        getLastGuide(selcust,false);
        });

    $("#store").on('change',function(event,param){
        var selcust = $(this). children("option:selected"). val();
        getOcurreInfo(selcust);
        });

    $("#spbank").on('change',function(event,param){
            var sel = $(this). children("option:selected"). val();
            $("#spaccount").children().remove().end();
            var def="";
            //console.log(param);
            if(param!=undefined)
                def=param;
            getCatalog("GetHint.aspx/GetCatalog","spaccount","bank_accounts","bank_account_id","name","",def,"bank_id="+sel+"","ORDER BY name",""); 
            });

    $(document).on('blur', '.ln-leave', function(){
        var subtotal=0;
        var discTot=0;
        var tax=0;
        var total=0;
        var desc=0;
        var qty=0;
        var subln=0;
        var subcdesc=0;
        var txPct=0;
        var totln=0;
        $('.ln-price').each(function(i, obj) {
            qty=document.getElementsByName("["+i+"][lnqty]")[0].value;
            desc=document.getElementsByName("["+i+"][lndiscount]")[0].value;
            txPct=document.getElementsByName("["+i+"][lntax]")[0].value;
            //console.log(i,obj);
            subln=+qty*+obj.value;
            document.getElementsByName("["+i+"][lntotal]")[0].value=round(subln,2);
            subcdesc = subln-desc;
            totln=subcdesc*(1 + (txPct/100));
            subtotal+=subln;
            var totivared = subcdesc + round(subcdesc * (txPct / 100), 2);
            //console.log(totln + " " + totivared)
         if (round(totln, 2) > round(totivared, 2))
             tax += round(subcdesc * ((txPct) / 100), 2) + .01;
        else if (round(totln, 2) < round(totivared, 2))
            tax += round(subcdesc * (txPct / 100), 2) - .01;
        else
             tax += round(subcdesc *  (txPct / 100), 2);
            discTot += +desc;
            //console.log(subtotal);
        });
        
        total = round(+subtotal-+discTot + +tax,2);
        $("[name='subtotal']").val(subtotal);
        $("[name='discount_amt']").val(discTot);
        $("[name='tax_amt']").val(tax);
        $("[name='total']").val(total);
    });
        
    function round(value, decimals) {
        return Number(Math.round(value+'e'+decimals)+'e-'+decimals);
      }

    if($("#actn").val()=="dash"){
        getQuotes(false);
    }
    if($("#actn").val()=="sinv"){
        getSaleInvoices();
    }
    if($("#actn").val()=="cxc"){
        getReceivableAccounts();
    }
    if($("#actn").val()=="cob"){
        getSalePayments();
    }
    if($("#actn").val()=="auth"){
        getCatalog("GetHint.aspx/GetCatalog","customer","customers","customer_id","name","code","","","ORDER BY name");
    }
    if ($("#actn").val() == "minvalmx") {
        var usrId = $("#userid").val();
        /*http://166.62.93.54/ProconecttWeb/pages/inicio.aspx?token=eyJhbGciOiJIUzI1NiIsInR5c...wjweufmqlt59f70383678m4dn*/
        getToken(usrId);
        $("#almexModal").modal("show");
        $('.modal-content').css('max-height', $(window).height() * 0.8);
       
        //$('#frame').attr("src", "");

    }
    console.log($("#actn").val());
    if($("#actn").val()=="minv"){
        if($("#params").val()!="")
            loadInvoice($("#params").val());
        else
        {
            getLastGuide(-1,true);
            //getCatalog("GetHint.aspx/GetCatalog","currency","currency","currency_id","code","name","1","","ORDER BY currency_id");
            //getCatalog("GetHint.aspx/GetCatalog","state","states","state_id","state_name","state_code","18","country_id=1","ORDER BY state_name","");
            //getCatalog("GetHint.aspx/GetCatalog","pay_method","payment_method","code","name","","01","","ORDER BY payment_method_id","");
            //getCatalog("GetHint.aspx/GetCatalog","pay_method_cond","pay_method","code","name","","PUE","","ORDER BY pay_method_id","");
            //getCatalog("GetHint.aspx/GetCatalog","cfdi_usage","cfdi_usage","code","name","","G03","","ORDER BY cfdi_usage_id","");
        }
    }
    GetNotifications();

    //setInterval (getChat, 2500);
    
});
