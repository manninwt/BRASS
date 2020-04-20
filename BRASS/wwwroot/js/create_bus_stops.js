function createStops(points) {

    //Create list of bust stops
    var busStops = [];

    //Remove points from the list until they are all associated with a stop
    while (points.length > 0) {

        point = points[0];
        var nonclustered = true;

        //If there are existing stops, see if the stop fits into any
        if (busStops.length > 0) {

            for (b of busStops) {

                //Iterate through cluster to determine a center point
                //NOTE: 100000 IS A DUMB HIGH VALUE SO THAT IT WILL ALWAYS BE REPLACED AS A CLOSEST DISTANCE, CHANGE IF NEED BE
                var centerP = [];
                var closestD = 100000;
                for (x of b) {
                    //Find the lowest sum of all distances from each point to see which fits best as the center
                    var d = 0;
                    for (y of b) {
                        d += Math.sqrt((y[0] - x[0]) * (y[0] - x[0]) + (y[1] - x[1]) * (y[1] - x[1]));
                    }
                    if (d < closestD) {
                        closestD = d;
                        centerP = x;
                    }
                }
                //Compare point to the center of the stop to see if it fits 
                //NOTE: THE 1.0 AT THE END OF THE IF STATEMENT IS A FILLER VALUE FOR THE MINIMUM DISTANCE A POINT SHOULD BE FROM THE CENTER, CHANGE TO BETTER VALUE
                if (Math.sqrt((centerP[0] - point[0]) * (centerP[0] - point[0]) + (centerP[1] - point[1]) * (centerP[1] - point[1])) < 1.0) {
                    //Add point to current stop
                    b.push(point);
                    //Remove point from the list of points 
                    points.shift();
                    //Set nonclustered to false so it does not create a new stop for this point
                    nonclustered = false;
                    //Break from loop so it does not check with other stops
                    break;
                }
            }
        }
        //If there are no existing stops, or the point did not fit into an existing stop, create a new stop for the point
        if (nonclustered) {
            //Add the point to its own new stop, and then remove it from the list
            var cluster = [point];
            busStops.push(cluster);
            points.shift();
        }
    }
    console.log(busStops);
}