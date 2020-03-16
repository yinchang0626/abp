$ErrorActionPreference = "Stop"

git subtree split -P angular/libs/MyProjectName -b angular/libs/MyProjectName

git push origin angular/libs/MyProjectName:angular/libs/MyProjectName

