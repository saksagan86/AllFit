import { useEffect } from "react"
import Map from "ol/Map";
import View from "ol/View";
import OSM from "ol/source/OSM";
import TileLayer from "ol/layer/Tile";
import { fromLonLat } from "ol/proj";
import VectorLayer from "ol/layer/Vector";
import VectorSource from "ol/source/Vector";
import Feature from "ol/Feature";
import { Point } from "ol/geom";
import { Icon, Style } from "ol/style";

function LocationsMap({coordinateLonLat}) {
    useEffect(() => {
        const layer = new VectorLayer({
            source: new VectorSource({
                features: [
                    new Feature({
                        geometry: new Point(fromLonLat(coordinateLonLat)),
                    })
                ]
            }),
            style: new Style({
                image: new Icon({
                    anchor: [0.5, 1],
                    crossOrigin: 'anonymous',
                    src: 'images/marker-icon.png'
                })
            })
        })

        const map = new Map({
            layers: [
                new TileLayer({
                    source: new OSM(),
                }),
                layer
            ],
            target: 'map',
            view: new View({
                center: fromLonLat(coordinateLonLat),
                zoom: 17,
            }),
        })

        return () => {
            map.setTarget(null)
        }
    })
    

    return (
        <div id="map" className="map"></div>
    )
}

export default LocationsMap