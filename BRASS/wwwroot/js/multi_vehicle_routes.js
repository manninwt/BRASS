var access_token = "";
var timeout = 1585715421022;

function getTime() {
    return new Date().getTime();
}

// Call without arguments controller(foo)
// Call with arguments controller(function(){ foo("Hello World!") })
async function controller(func) {
    var token
    token = await getToken();
    func()
}

// Call using a async function like controller
// so the await can be used, otherwise the
// token won't be ready by the time you make a call
function getToken() {

    // Get creditiionals from going to either https://developers.arcgis.com/dashboard or https://developers.arcgis.com/applications and creating an application
    // Need to switch these out perferrable with a stored value for each user in appsettings.json, but for the project these can be manually changed
    client_id = "";
    client_secret = "";

    grant_type = "client_credentials";

    var time = getTime();

    if (time > timeout) {
        var options = {
            method: 'POST',
            url: 'https://www.arcgis.com/sharing/rest/oauth2/token',
            headers:
                { 'content-type': 'application/x-www-form-urlencoded' },
            data:
            {
                client_id: client_id,
                client_secret: client_secret,
                grant_type: grant_type
            }
        };

        return new Promise(function (resolve, reject) {
            $.ajax(options).done(function (response) {
                // returns the access token response - even if the call fails to connect
                var msg = JSON.parse(response);
                console.log(msg);
                access_token = msg.access_token;
                timeout = time + (msg.expires_in * 1000);
                resolve(access_token)
            })
                .fail(function (xhr, status, error) {
                    console.log("Error in getToken: " + error);
                });
        })
    } else {
        return access_token
    }
};

// call using - controller(complexRouteAsync);
// expsive call in arcgis credits, please be sure you have all the right data
function complexRouteAsync() {
    var depots_info = {
        "type": "features",
        "features": [{
            "attributes": {
                "Name": "Bay Cities Kitchens & Appliances"
            },
            "geometry": {
                "x": -118.469630,
                "y": 34.037555
            }
        }]
    };

    var route_info = {
        "features": [{
            "attributes": {
                "Name": "Route 1",
                "Description": "vehicle 1",
                "StartDepotName": "Bay Cities Kitchens & Appliances",
                "EndDepotName": "Bay Cities Kitchens & Appliances",
                "Capacities": "4",
                "MaxOrderCount": 3,
                "MaxTotalTime": 60,
            }
        }
            ,
        {
            "attributes": {
                "Name": "Route 2",
                "Description": "vehicle 2",
                "StartDepotName": "Bay Cities Kitchens & Appliances",
                "EndDepotName": "Bay Cities Kitchens & Appliances",
                "Capacities": "4",
                "MaxOrderCount": 3,
                "MaxTotalTime": 60,
            }
        }
        ]
    };

    var order_info = {
        "type": "features",
        "features": [{
            "attributes": {
                "Name": "Father's Office",
                "ServiceTime": 10
            },
            "geometry": {
                "x": -118.498406,
                "y": 34.029445
            }
        },
        {
            "attributes": {
                "Name": "R+D Kitchen",
                "ServiceTime": 10
            },
            "geometry": {
                "x": -118.495788,
                "y": 34.032339
            }
        },
        {
            "attributes": {
                "Name": "Pono Burger",
                "ServiceTime": 10
            },
            "geometry": {
                "x": -118.489469,
                "y": 34.019000
            }
        },
        {
            "attributes": {
                "Name": "Il Ristorante di Giorgio Baldi",
                "ServiceTime": 10
            },
            "geometry": {
                "x": -118.518787,
                "y": 34.028508
            }
        },
        {
            "attributes": {
                "Name": "Milo + Olive",
                "ServiceTime": 10
            },
            "geometry": {
                "x": -118.476026,
                "y": 34.037572
            }
        },
        {
            "attributes": {
                "Name": "Dialogue",
                "ServiceTime": 10
            },
            "geometry": {
                "x": -118.495814,
                "y": 34.017042
            }
        }
        ]
    };

    var options = {
        "url": "https://logistics.arcgis.com/arcgis/rest/services/World/VehicleRoutingProblem/GPServer/SolveVehicleRoutingProblem/submitJob",
        "method": "POST",
        "timeout": 0,
        "headers":
            { 'content-type': 'application/x-www-form-urlencoded' },
        "data": {
            "f": "json",
            "token": access_token,
            "populate_direction": "true",
            "uturn_policy": "NO_UTURNS",
            "depots": JSON.stringify(depots_info),
            "routes": JSON.stringify(route_info),
            "orders": JSON.stringify(order_info),
            "default_date": "1585715421022",
            "expires_in": "7200"
        }
    };

    $.ajax(options)
        .done(function (response) {
            jobId = response.jobId;
            waitForJobComplete(jobId);
        })
        .fail(function (xhr, status, error) {
            console.log("Error in complexRouteAsync: " + error);
        });
}

// call using - controller(simpleRoute);
function simpleRoute() {
    var stops = {
        "type": "features",
        "features": [
            {
                "geometry": {
                    "x": -84.501,
                    "y": 39.134
                },
                "attributes": {
                    "Name": "Start Location"
                }
            },
            {
                "geometry": {
                    "x": -84.502,
                    "y": 39.131
                },
                "attributes": {
                    "Name": "Stop 1"
                }
            },
            {
                "geometry": {
                    "x": -84.507,
                    "y": 39.128
                },
                "attributes": {
                    "Name": "Stop 2"
                }
            },
            {
                "geometry": {
                    "x": -84.516,
                    "y": 39.126
                },
                "attributes": {
                    "Name": "Stop 3"
                }
            },
            {
                "geometry": {
                    "x": -84.521,
                    "y": 39.126
                },
                "attributes": {
                    "Name": "Stop 4"
                }
            },
            {
                "geometry": {
                    "x": -84.528,
                    "y": 39.130
                },
                "attributes": {
                    "Name": "Stop 5"
                }
            },
            {
                "geometry": {
                    "x": -84.524,
                    "y": 39.145
                },
                "attributes": {
                    "Name": "End"
                }
            }
        ]
    };

    var options = {
        url: 'https://route.arcgis.com/arcgis/rest/services/World/Route/NAServer/Route_World/solve',
        method: 'POST',
        timeout: 0,
        headers:
        {
            'content-type': 'application/x-www-form-urlencoded'
        },
        data: {
            f: 'json',
            token: access_token,
            populate_direction: 'true',
            uturn_policy: 'NO_UTURNS',
            stops: JSON.stringify(stops)
        }
    };

    $.post(options)
        .done(function (response) {
            console.log(response);
        })
        .fail(function (xhr, status, error) {
            console.log("Error in simpleRoute: " + error);
        });
}


function checkRouteProgress(job) {
    var options = {
        method: 'POST',
        url: 'https://logistics.arcgis.com/arcgis/rest/services/World/VehicleRoutingProblem/GPServer/SolveVehicleRoutingProblem/jobs/' + job + '?',
        headers:
            { 'content-type': 'application/x-www-form-urlencoded' },
        data:
        {
            f: 'json',
            token: access_token
        }
    };

    return new Promise(function (resolve, reject) {
        $.ajax(options)
            .done(function (response) {
                resolve(response.jobStatus);
            })
            .fail(function (xhr, status, error) {
                console.log("Error in checkRouteProgress: " + error);
                reject(error);
            });
    });
}

function getCompletedRoutes(job) {
    var options = {
        method: 'POST',
        url: 'https://logistics.arcgis.com/arcgis/rest/services/World/VehicleRoutingProblem/GPServer/SolveVehicleRoutingProblem/jobs/' + job + '/results/out_routes?',
        headers:
            { 'content-type': 'application/x-www-form-urlencoded' },
        data:
        {
            f: 'json',
            token: access_token
        }
    };

    $.ajax(options)
        .done(function (response) {
            console.log("Success, getCompletedRoutes")
            console.log(response);
        })
        .fail(function (xhr, status, error) {
            console.log("Error in getCompletedRoutes: " + error);
        });
}

async function waitForJobComplete(job) {

    await getToken();
    var jobStatus = "";
    while (true) {
        jobStatus = await checkRouteProgress(job);
        console.log(jobStatus)
        if (jobStatus == "esriJobSucceeded") {
            console.log("Job completed succesfully");
            getCompletedRoutes(job);
            break;
        }
        else if (jobStatus.match(/^(esriJobFailed|esriJobTimedOut|esriJobCancelled)$/)) {
            // send notification that job did not complete correctly
            console.log(jobSatus + ": Job did not completed succesfully")
            break;
        }
        setTimeout(4000);
    }
}