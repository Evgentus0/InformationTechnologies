import { Injectable } from '@angular/core';

@Injectable()
export class HelperService{

    fromArrayToObject(data:any[], colNames:string[]){
        let result = {};
        result["_id_guid"] = data[0];
        for(let i=0;i<colNames.length;i++){
            result[colNames[i]] = data[i + 1];
        }
        return result;
    }
}