import {useParams} from "react-router-dom";

import { useCocPlayerQuery, useCrPlayerQuery, useBsPlayerQuery } from "../api/apiSlice"
import CocPlayer from "./CocPlayer";

export default function Player ({type}: {type: number}) { 
    let { tag } = useParams();

    const { data : cocplayer } = useCocPlayerQuery(tag);
    const { data : crplayer } = useCrPlayerQuery(tag);
    const { data : bsplayer } = useBsPlayerQuery(tag);
    
    
    if (type === 0 && cocplayer !== undefined) {
        console.log(crplayer);
        return (<div>{type} {tag} {cocplayer.attackWins}</div>)
    }
    if (type === 1 && crplayer !== undefined) {
        console.log(cocplayer);
        return (<div>{type} {tag}</div>)
    }
    if (type === 2 && bsplayer !== undefined) {
        console.log(bsplayer);
        return (<div>{type} {tag}</div>)
    }
    
    return (<div>Error</div>)
}