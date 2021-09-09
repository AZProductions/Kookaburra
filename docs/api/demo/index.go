// This file was generated from JSON Schema using quicktype, do not modify it directly.
// To parse and unparse this JSON data, add this code to your project and do:
//
//    newestVersion, err := UnmarshalNewestVersion(bytes)
//    bytes, err = newestVersion.Marshal()

package main

import "encoding/json"

type NewestVersion []NewestVersionElement

func UnmarshalNewestVersion(data []byte) (NewestVersion, error) {
	var r NewestVersion
	err := json.Unmarshal(data, &r)
	return r, err
}

func (r *NewestVersion) Marshal() ([]byte, error) {
	return json.Marshal(r)
}

type NewestVersionElement struct {
	URLWin64    interface{} `json:"url_win64"`   
	URLWin86    interface{} `json:"url_win86"`   
	URLWinarm   interface{} `json:"url_winarm"`  
	URLLinux    interface{} `json:"url_linux"`   
	URLLinuxarm interface{} `json:"url_linuxarm"`
	Version     string      `json:"version"`     
	Date        string      `json:"date"`        
}
