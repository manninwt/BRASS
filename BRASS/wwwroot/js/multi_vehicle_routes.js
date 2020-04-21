var access_token = "";
var timeout = 1585715421022;

function getTime() {
    return new Date().getTime();
}

// Call without arguments controller(foo)
// Call with arguments controller(function(){ foo("Hello World!") })
async function controller(func) {
    var token
    token = await getTokenStatic();
    func()
}

// Call using a async function like controller
// so the await can be used, otherwise the
// token won't be ready by the time you make a call
function getTokenStatic() {

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


function GetAllStopValues() {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: "/Home/GetAllStopValues",
            data: {},
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            success: function (data, status, xhr) {
                resolve(data);
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
            }
        })
    });
}

function GetSchoolValues() {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: "/Home/GetSchoolValues",
            data: {},
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            success: function (data, status, xhr) {
                resolve(data);
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
            }
        })
    });
}

function GetBusValues() {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: "/Home/GetBusValues",
            data: {},
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            success: function (data, status, xhr) {
                resolve(data);
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
            }
        })
    });
}

function GetDriverValues() {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: "/Home/GetDriverValues",
            data: {},
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            success: function (data, status, xhr) {
                resolve(data);
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
            }
        })
    });
}

function GetDriverRoute(x) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: "/Home/GetDriverRoute",
            data: { "id": parseInt(x) },
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            success: function (data, status, xhr) {
                resolve(data);
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
            }
        })
    });
}

function GetRoutePointsValues() {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: "/Home/GetRoutePoints",
            data: {},
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            success: function (data, status, xhr) {
                resolve(data);
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
            }
        })
    });
}

function routeTimeFeature(name, longitude, lattitude, ServiceTime = 10){
    return {
                "attributes": {
                    "Name": name,
                    "ServiceTime": ServiceTime
                },
                "geometry": {
                    "x": longitude,
                    "y": lattitude
                }
            }
}

function routeFeature(name, longitude, lattitude) {
    if (!isNaN(name)) {
        name = name.toString(10)
    }
    return {
                "attributes": {
                    "Name": name
                },
                "geometry": {
                    "x": longitude,
                    "y": lattitude
                }
            }
}

async function GetMultiRouteInfo() {
    var info = {
        depots_info: {
            "type": "features",
            "features": []
        },
        route_info: {
            "features": []
        },
        order_info: {
            "type": "features",
            "features": []
        }
    }
    var stops = await GetAllStopValues();
    var school = await GetSchoolValues();
    var busses = await GetBusValues();
    var drivers = await GetDriverValues();

    var schoold_depot_name = school[0].schoolName;
    info.depots_info.features.push(routeFeature(schoold_depot_name, school[0].longitude, school[0].lattitude))
    for (i = 0; i < drivers.length; i++) {
        if (drivers[i].condition == "ACTIVE") {
            var driver_depot_name = drivers[i].driverId;
            
            info.route_info.features.push({
                "attributes": {
                    "Name": i.toString(10),
                    "Description": "vehicle " + i,
                    "StartDepotName": driver_depot_name,
                    "EndDepotName": schoold_depot_name,
                    "Capacities": "30",
                    "MaxOrderCount": 30,
                    "MaxTotalTime": 60,
                }
            })
            info.depots_info.features.push(routeFeature(driver_depot_name, drivers[i].longitude, drivers[i].lattitude))
        }
    }
    for (i = 0; i < stops.length; i++) {
        if (stops[i].longitude != 0 && stops[i].lattitude != 0) {
            info.order_info.features.push(routeTimeFeature(stops[i].stopId, stops[i].longitude, stops[i].lattitude, 10))
        }
    }

    ArcGisAPIController(complexRouteAsync, [info])
}

async function GetAddedStudentInfo() {
    var stops = await GetAllStopValues();
    var routePoints = await GetRoutePointsValues();
    var school = await GetSchoolValues();

    var routes = {}
    var addedStops = [], max = -Infinity, key;
    for (i = 0; i < stops.length; i++) {
        if (stops[i].longitude != 0 && stops[i].lattitude != 0) {
            if (stops[i].routeId == 0) {
                addedStops.push({ "stopId": stops[i].stopId, "stopNumber": stops[i].stopNumber, "routeId": 0, "longitude": stops[i].longitude, "lattitude": stops[i].lattitude, minDist: 0 })
            } else if (routes[stops[i].routeId]) {
                routes[stops[i].routeId][stops[i].stopNumber] = { "stopId": stops[i].stopId, "stopNumber": stops[i].stopNumber, "longitude": stops[i].longitude, "lattitude": stops[i].lattitude }
            } else {
                var stop = {}
                stop[stops[i].stopNumber] = {
                    "stopId": stops[i].stopId,
                    "stopNumber": stops[i].stopNumber,
                    "longitude": stops[i].longitude,
                    "lattitude": stops[i].lattitude
                }
                routes[stops[i].routeId] = stop
            }
        }
    }

    if (addedStops.length == 0) {
        return;
    }

    // Figures out which route each new stop will be added to
    for (i = 0; i < routePoints.length; i++) {
        for (j = 0; j < addedStops.length; j++) {
            var distance = Math.sqrt((routePoints[i].longitude - addedStops[j].longitude) ** 2 + (routePoints[i].lattitude - addedStops[j].lattitude) ** 2);
            if (addedStops[j].minDist != 0 && addedStops[j].minDist > distance) {
                addedStops[j].minDist = distance
                addedStops[j].routeId = routePoints[i].routeId
            } else if (addedStops[j].minDist == 0){
                addedStops[j].minDist = distance
                addedStops[j].routeId = routePoints[i].routeId
            }
        }
    }

    var info = {
        "type": "features",
        "features": []
    }
    var routeInfo = {};
    var stopInfo = {};
    // Figures out where in the route each new stop will be
    for (i = 0; i < addedStops.length; i++) {
        addedStops[i].minDist = 0
        var routeId = addedStops[i].routeId

        var driver = await GetDriverRoute(addedStops[i].routeId);

        // create the routeInfo for route
        var createNewRouteInfo = false;
        if (!routeInfo[routeId]) {
            createNewRouteInfo = true;
            routeInfo[routeId] = info
            stopInfo[routeId] = []
            //routeInfo[routeId].features.push(routeFeature(driver.driverId, driver.longitude, driver.lattitude));
        }
        

        for (j = 1; j <= Object.keys(routes[routeId]).length; j++) {
            if (createNewRouteInfo) {
                routeInfo[routeId].features.push(routeFeature(routes[routeId][j].stopId, routes[routeId][j].longitude, routes[routeId][j].lattitude));
                stopInfo[routeId].push(routes[routeId][j].stopId)
            }
            var distance = Math.sqrt((addedStops[i].longitude - routes[routeId][j].longitude) ** 2 + (addedStops[i].lattitude - routes[routeId][j].lattitude) ** 2);
            if (addedStops[i].minDist != 0 && addedStops[i].minDist > distance) {
                addedStops[i].minDist = distance
                addedStops[i].stopNumber = j
            } else if (addedStops[i].minDist == 0){
                addedStops[i].minDist = distance
                addedStops[i].stopNumber = j
            }
        }
        if (createNewRouteInfo) {
            routeInfo[routeId].features.push(routeFeature(school[0].schoolName, school[0].longitude, school[0].lattitude))
        }
    }

    // Add the new stops their corresponding route
    while (addedStops.length > 0) {
        addedStops.forEach(function (v, k) {
            if (max < +v.stopNumber) {
                max = +v.stopNumber;
                key = k;
            }
        });
        routeInfo[addedStops[key].routeId].features.splice((addedStops[key].stopNumber), 0, routeFeature(addedStops[key].stopId, addedStops[key].longitude, addedStops[key].lattitude))
        stopInfo[routeId].splice((addedStops[key].stopNumber), 0, addedStops[key].stopId)
        addedStops.splice(key, 1)
        max = -Infinity
    }

    for (let infoKey in routeInfo) {
        ArcGisAPIController(simpleRoute, [routeInfo[infoKey], stopInfo[infoKey], infoKey])
    }
    
}

// call using - ArcGisAPIController(complexRouteAsync, [info])
// expsive call in arcgis credits, please be sure you have all the right data
async function complexRouteAsync(token, info) {
    access_token = token
    var options = {
        "url": "https://logistics.arcgis.com/arcgis/rest/services/World/VehicleRoutingProblem/GPServer/SolveVehicleRoutingProblem/submitJob",
        "method": "POST",
        "timeout": 0,
        "headers":
            { 'content-type': 'application/x-www-form-urlencoded' },
        "data": {
            "f": "json",
            "token": token,
            "populate_direction": "true",
            "uturn_policy": "NO_UTURNS",
            "depots": JSON.stringify(info.depots_info),
            "routes": JSON.stringify(info.route_info),
            "orders": JSON.stringify(info.order_info),
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

// call using - ArcGisAPIController(simpleRoute, [stops])
function simpleRoute(token, stops, stopInfo, routeId) {
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
            token: token,
            populate_direction: 'true',
            uturn_policy: 'NO_UTURNS',
            stops: JSON.stringify(stops)
        }
    };

    $.post(options)
        .done(function (response) {
            var routePaths = response.routes.features[0].geometry.paths
            setAddedRouteValues(routePaths, stopInfo, routeId)
        })
        .fail(function (xhr, status, error) {
            console.log("Error in simpleRoute: " + error);
        });
}

async function setAddedRouteValues(routePaths, stopInfo, routeId) {
    await removeRoutePointsForRoute(routeId)
    for (i = 0; i < stopInfo.length; i++) {
        await SetStopInfo(stopInfo[i], i + 1, routeId)
    }
    for (i = 0; i < routePaths.length; i++) {
        for (j = 0; j < routePaths[i].length; j++) {
            var longitude = routePaths[i][j][0]
            var lattitude = routePaths[i][j][1]
            await setRoutePoints(longitude, lattitude, routeId)
        }
    }
}

function removeRoutePointsForRoute(routeId) {
    return new Promise(function (resolve, reject) {
    $.ajax({
        url: "/Home/RemoveRoutePointsForRoute",
        data: { "routeId": routeId },
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        success: function (data, status, xhr) {
            resolve()
        },
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            alert(err.Message);
        }
    })
    });
}

function setRoutePoints(longitude, lattitude, routeId) {
    return new Promise(function (resolve, reject) {
    $.ajax({
        url: "/Home/SetRouteInfo",
        data: { "longitude": longitude, "lattitude": lattitude, "routeId": routeId},
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        success: function (data, status, xhr) {
            resolve()
        },
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            alert(err.Message);
        }
    })
    });
}

function SetStopInfo(stopId, stopNumber, routeId) {
    return new Promise(function (resolve, reject) {
    $.ajax({
        url: "/Home/SetStopInfo",
        data: { "stopId": stopId, "stopNumber": stopNumber, "routeId": routeId},
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        success: function (data, status, xhr) {
            resolve()
        },
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            alert(err.Message);
        }
    })
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

    return new Promise(function (resolve, reject) {
        $.ajax(options)
            .done(function (response) {
                console.log("Success, getCompletedRoutes")
                resolve(response);
            })
            .fail(function (xhr, status, error) {
                console.log("Error in getCompletedRoutes: " + error);
            });
    })
}

function getCompletedStops(job) {
    var options = {
        method: 'POST',
        url: 'https://logistics.arcgis.com/arcgis/rest/services/World/VehicleRoutingProblem/GPServer/SolveVehicleRoutingProblem/jobs/' + job + '/results/out_stops?',
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
                console.log("Success, getCompletedStops")
                resolve(response);
            })
            .fail(function (xhr, status, error) {
                console.log("Error in getCompletedStops: " + error);
            });
    })
}

async function waitForJobComplete(job) {

    await getToken();
    var jobStatus = "";
    while (true) {
        jobStatus = await checkRouteProgress(job);
        console.log(jobStatus)
        if (jobStatus == "esriJobSucceeded") {
            console.log("Job completed succesfully");
            routes = await getCompletedRoutes(job);
            stops = await getCompletedStops(job);
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