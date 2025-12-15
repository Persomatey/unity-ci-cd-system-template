# Unity CI/CD System Template 
A simple free template CI/CD system using [GameCI](https://hub.docker.com/u/unityci) for Unity games using GitHub Actions. 

You can check out the sample app on the [Releases tab](https://github.com/Persomatey/unity-ci-cd-system-template/releases) of this GitHub repo. 

## Features 
- GitHub Releases
     - Builds get submitted to the "Releases" tab of your repo as a new release with separate .zip files for each build. 
- Version numbers, last Commit SHAs, and defines are added to the project via a .json file.
     - `\Assets\Scripts\Versioning\versioning.json` in the project which can be displayed in game (on a main menu or something if you want).
     - Showcased in the Unity project scene.
- Unity Build Profiles
     - Under the `buildForAllSupportedPlatforms` job, you can change the `strategy`'s `matrix` and include whatever build profiles you want.
     - Showcased in the differences between the built Unity projects, including the defines included in the Build Profiles as displayed in the Unity project scene.
- Supports [semantic versioning](https://semver.org/) (MAJOR.MINOR.PATCH).
     - Every push increments the PATCH number, with MAJOR and MINOR being incremented maually. 
- *(Optional)* Parallel builds (to speed up development, but may need to be turned off if memory is exceeding what your runner supports).  
     - Under the `buildForAllSupportedPlatforms` job, you can change the `strategy`'s `max-parallel` value accordingly.
- *(Optional)* Fail fast support, so you're not creating multiple builds if one fails.
     - Under the `buildForAllSupportedPlatforms` job, you can change the `strategy`'s `fail-fast` accordingly.
     - It's set as `false` by default because sometimes there could be a problem with a single build profile or platform -- but it's there if you're stingy with your runner minutes. 
- *(Optional)* LFS support
     - Under the `Checkout repository` step, change the `lfs` value accordingly. 
- *(Optional)* Concurrent workflows 
     - Under `concurrency`, set the `cancel-in-progress` value accordingly.
     - This is mostly to save on runner minutes, but if you don't care about that, leaving it `false` allows you to better track down a bug, especially when collaborating with multiple devs or if you have long build times. 


## Workflows 

### Build (`build.yml`)
Every time a push is made to the GitHub repository, builds will trigger using the Unity BuildProfiles files provided in the `build.yml`. This will also increment the PATCH version number. A Release Tag will be generated and the builds generated will be included in your repo page's "Releases" tab. 

Build profiles included by default: 
- `windows-dev`: Dev build for Windows with DEV defines included 
- `windows-rel`: Release build for Windows with REL defines included 
- `linux-dev`: Dev build for Linux with DEV defines included 
- `linux-rel`: Release build for Linux with REL defines included 
- `webgl-dev`: Dev build for WebGL with DEV defines included 
- `webgl-rel`: Release build for WebGL with REL defines included 

### Versioning (`version-bump.yml`)
Used to manually version bump the version number. Should be in the format `X.Y.Z`. All future pushes will subsequently start incrementing based on the new MAJOR or MINOR version changes. 
  - Ex: If the last version before triggering this workflow is `v0.0.42`, and the workflow was triggered with `v0.1.0`, the next `build.yml` workflow run will create the version tag `v0.1.1`. 

## Set up  
1. Find/Generate Unity license
    1. Open Unity Hub and log in with your Unity account (if you do not have a current .ulf) then navigate to Preferences > Licenses > Add) 
    2. Find your `Unity_lic.ulf` file
        - Windows: `C:\ProgramData\Unity\Unity_lic.ulf`
        - Mac: `/Library/Application Support/Unity/Unity_lic.ulf`
        - Linux: `~/.local/share/unity3d/Unity/Unity_lic.ulf`
2. Hook up Unity Credentials 
    1. On your GitHub repo's, navigate to Setting > Secrets and variables > Actions
    2. Create three new Repository secrets
        - `UNITY_LICENSE` (Paste the contents of your license file into here)
        - `UNITY_EMAIL` (Add the email address that you use to log into Unity)
        - `UNITY_PASSWORD` (Add the password that you use to log into Unity)
3. Create initial version tag
     1. Navigate to your GitHub version tags page `github.com/username_or_org/repo_name/releases/new`
     2. Click "Tag: Select Tag"
     3. Set tag to v0.0.0
     4. Click "Create"
     5. Set "Release title"
     6. Click "Publish release" 
4. Copy the workflows located in this repo's `.github/workflows/` into your `.github/workflows/` (create this directory if you don't have one already
   - `build.yml`
   - `version-bump.yml`
5. In `build.yml`'s `buildForAllSupportedPlatforms` step, include the Unity Build Profiles you want generated
6. In `build.yml`'s `Build with Unity (Build Profile)` step, set the `projectPath` variable to your project folder ????????????????????????????????
7. In `build.yml`'s `Build with Unity (Build Profile)` step, set the `unityVersion` variable to the version of Unity you're using ?????????????????????????????
    - Ensure it uses a version of Unity that GameCI supports on their [tags page](https://hub.docker.com/r/unityci/editor/tags)
8. In `build.yml`, in the `env`, set the `PROJECT_NAME` variable to your project's name. 
9. In `build.yml`, in the `env`, set the `UNITY_VERSION` variable to your project's Unity version. 
10. In `build.yml`, in the `env`, set the `PROJECT_PATH` variable to your project's path. 

## Future Plans 
*No plans on when I'd release these features, would likely depend on my needs for a specific project/boredom/random interest in moving this project along.*
- Include multiple workflow concurrency
- Include platform and included defines in .json
- Android build support
- iOS build support
- VR build support 
- itch.io CD
- Steam CD
- Epic Games CD 
- Slack notifications webhook
- Google Meets notifications webhook
- Discord notifications webhook
- Microsoft Teams notifications webhook
- Add more concurrency features for multiple in progress workflows 
