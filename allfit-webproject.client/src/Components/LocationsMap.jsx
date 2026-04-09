import { useEffect } from "react"
import Map from "ol/Map";
import View from "ol/View";
import OSM from "ol/source/OSM";
import TileLayer from "ol/layer/Tile";

function LocationsMap() {
    useEffect(() => {
        const map = new Map({
            layers: [
                new TileLayer({
                    source: new OSM(),
                }),
            ],
            target: 'map',
            view: new View({
                center: [485895.87883085536, 6799262.320082145],
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