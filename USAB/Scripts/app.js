var App = function () {

    var handlerValidators = function () {
        var bootstrap = $('#register_form').bootstrapValidator({
            excluded: [':disabled', ':hidden', ':not(:visible)'],
            submitButtons: 'input[type="submit"]',
            message: 'This value is not valid',
            feedbackIcons: {
                valid: 'glyphicon glyphicon-ok',
                invalid: 'glyphicon glyphicon-remove',
                validating: 'glyphicon glyphicon-refresh'
            },
            live: 'enabled',
            fields: {
                email: {
                    validators: {
                        notEmpty: {
                            message: 'The email address is required and can\'t be empty'
                        },
                        emailAddress: {
                            message: 'The input is not a valid email address'
                        }
                    }
                },
                website: {
                    validators: {
                        uri: {
                            message: 'The input is not a valid URL'
                        }
                    }
                },
                phoneNumber: {
                    validators: {
                        digits: {
                            message: 'The value can contain only digits'
                        }
                    }
                },
                color: {
                    validators: {
                        hexColor: {
                            message: 'The input is not a valid hex color'
                        }
                    }
                },
                zipCode: {
                    validators: {
                        zipCode: {
                            country: 'US',
                            message: 'The input is not a valid US zip code'
                        }
                    }
                },
                password: {
                    validators: {
                        notEmpty: {
                            message: 'The password is requiredd and can\'t be empty'
                        },
                        identical: {
                            field: 'confirmPassword',
                            message: 'The password and its dconfirm are not the same'
                        }
                    }
                },
                confirmPassword: {
                    validators: {
                        notEmpty: {
                            message: 'The confirm password idds required and can\'t be empty'
                        },
                        identical: {
                            field: 'password',
                            message: 'The password and its codddnfirm are not the same'
                        }
                    }
                },
                passwordASP: {
                    validators: {
                        notEmpty: {
                            message: 'The confirm password idds required and can\'t be empty'
                        },
                        identical: {
                            field: 'passwordASP2',
                            message: 'The password and its codddnfirm are not the same'
                        }
                    }
                },
                passwordASP2: {
                    validators: {
                        notEmpty: {
                            message: 'The confirm password idds required and can\'t be empty'
                        },
                        identical: {
                            field: 'passwordASP',
                            message: 'The password and its codddnfirm are not the same'
                        }
                    }
                }

            },
            submitHandler: function (validator, form, submitButton) {
                // validator is the BootstrapValidator instance
                // form is the jQuery object present the current form

                // btnSalvarAlteracoes_Click
                //form.submit();
                __doPostBack('btnsubmit', '');

            },


        });



    };

    var handlerMenu = function () {
        $(function () {
            $('#menucontent ul').find('> li').hover(function () {
                $(this).find('ul')
        .removeClass('noJS')
        .stop(true, true).slideToggle('fast');
            });
        });
    };

    var handlerSlides = function () {
        $(function () {
            $("#slides").slides({
                preload: true,
                preloadImage: '/img/loading.gif',
                play: 5000,
                pause: 2500,
                hoverPause: true,
                animationStart: function (current) {
                    $('.caption').animate({
                        bottom: -35
                    }, 100);
                    if (window.console && console.log) {
                        // example return of current slide number
                        console.log('animationStart on slide: ', current);
                    }
                },
                animationComplete: function (current) {
                    $('.caption').animate({
                        bottom: 0
                    }, 200);
                    if (window.console && console.log) {
                        // example return of current slide number
                        console.log('animationComplete on slide: ', current);
                    }
                },
                slidesLoaded: function () {
                    $('.caption').animate({
                        bottom: 0
                    }, 200);
                }
            });
        });
    };

    var handlerESPN = function () {
        $.ajax({
            url: "http://api.espn.com/v1/sports/basketball/nba/news/headlines",
            data: {
                // enter your developer api key here
                apikey: "4p3dsw5ztw9dqywrdmfrnmff",
                // the type of data you're expecting back from the api
                _accept: "application/json",

                limit: 5
            },
            dataType: "jsonp",
            beforeSend: function () {


            },
            success: function (data) {
                // create an unordered list of headlines
                var ul = $('<ul/>');
                var divtab = $('<div/>');

                $.each(data.headlines, function () {

                    var li = $('<li/>');
                    var a = $('<a/>').attr('href', '#' + this.id);

                    a.text(this.headline).attr("style", "font-size:10px");
                    var div = $('<div/>');
                    div.attr('id', this.id);

                    var linkimg = $('<a/>');

                    linkimg.attr('href', this.links.web.href);

                    var img = $('<img/>');
                    if (this.images[0] !== null) {
                        img.attr('src', this.images[0].url);
                    }
                    linkimg.append(img);
                    div.append('<p>' + this.description + '</p>');

                    if (img[0].href !=='') {
                        div.append(linkimg);
                    }

                    li.append(a);
                    ul.append(li);
                    divtab.append(div);
                });

                var ESPNHEADER = $('#espnHeadlinesNBA');
                // append this list to the document body
                ESPNHEADER.append(ul);
                ESPNHEADER.append(divtab);

                ESPNHEADER.tabs({
                    beforeLoad: function (event, ui) {
                        ui.jqXHR.error(function () {
                            ui.panel.html(
                              "Couldn't load this tab. We'll try to fix this as soon as possible. " +
                              "If this wouldn't be a demo.");
                        });
                    }
                });




            },
            error: function () {
                // handle the error
            }
        });
        $.ajax({
            url: "http://api.espn.com/v1/sports/soccer/fifa.world/news/headlines/top/",
            data: {
                // enter your developer api key here
                apikey: "4p3dsw5ztw9dqywrdmfrnmff",
                // the type of data you're expecting back from the api
                _accept: "application/json",

                limit: 4
            },
            dataType: "jsonp",
            success: function (data) {
                // create an unordered list of headlines
                var div = $('.News_Content');

                var accordDIV = $("<div />", { id: "accordion" });


                $.each(data.headlines, function () {

                    var h3 = $("<h3/>", { text: this.title });
                    accordDIV.append(h3);

                    var secDIV = $("<div />");

                    var para = $("<p/>");



                    if (this.video.length > 0) {

                        var herf = $('<a/>', { href: this.video[0].links.web.href });
                        var img = $('<img/>', { src: this.video[0].thumbnail, height: '300px', width: '500px' });

                        herf.append(img);
                        para.append(herf);
                    }

                    secDIV.append(para.append(this.description));
                    accordDIV.append(secDIV);

                });

                div.append(accordDIV);
                $("#accordion").accordion({ heightStyle: "content" });
            },


        });
    };

    var handlerAccordion = function () {

        //$("#accordion").accordion();

    };

    var handlerAD = function () {
        setInterval(function () {
            $("[id$=adr]").load(location.href + " [id$=adr]", "" + Math.random() + "").hide().fadeIn('slow');
        }, 4000);
    };

    var handlerSlideImages = function () {
        $.ajax({
            url:"../Images/SlideImages/",
            success: function(data)
            {
                $(data).find("a:contains(.jpg)").each(function (i) {
                    var filename = this.pathname;
                    $(".slides_container").append("<div><img src='" + filename + "' width='1077' height='300' alt='' /></div>")
                })

                handlerSlides();
            }
        });
    }

    return {

        init: function () {
            handlerSlideImages();
            handlerAccordion();
            handlerMenu();
         

        },

        GetEPSNNEWS: function () {
            handlerESPN();
        },

        BootstrapValidator: function () {
            handlerValidators();
        },

        LoginBootStrapVal: function () {

            var bootstrap1 = $('#login_form').bootstrapValidator({
                excluded: [':disabled', ':hidden', ':not(:visible)'],
                submitButtons: 'input[type="submit"]',
                message: 'This value is not valid',
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                live: 'enabled',
                fields: {
                    email: {
                        validators: {
                            notEmpty: {
                                message: 'The email address is required and can\'t be empty'
                            },
                            emailAddress: {
                                message: 'The input is not a valid email address'
                            }
                        }
                    }
                },
                submitHandler: function (validator, form, submitButton) {
                    // validator is the BootstrapValidator instance
                    // form is the jQuery object present the current form
                    __doPostBack('btnsubmit', '');

                },
            });



        }




    };

}();