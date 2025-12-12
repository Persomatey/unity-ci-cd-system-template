# unity-ci-cd-system-template
Meant to serve as a simple template CI/CD system using GameCI for Unity games using GitHub Actions. Supports [semantic versioning](https://semver.org/) (MANOR.MINOR.PATCH) with every push incrementing the PATCH number, with MAJOR and MINOR being incremented maually. This supports Unity 6's Build Profiles feature off the bat 

## Workflows 

### Build (`build.yml`)
Every time a push is made to the GitHub repository, builds will trigger using the Unity BuildProfiles files provided in the `build.yml`. This will also increment the PATCH version number. A Release Tag will be generated and the builds generated will be included in your repo page's "Releases" tab. 

Build profiles included by default: 
- `windows-dev`: Dev build for Windows with DEV definsed included 
- `windows-rel`: Release build for Windows with REL defindes included 
- `linux-dev`: Dev build for Linux with DEV definsed included 
- `linux-rel`: Release build for Linux with REL defindes included 
- `webgl-dev`: Dev build for WebGL with DEV definsed included 
- `webgl-rel`: Release build for WebGL with REL defindes included 

### Versioning (`version-bump.yml`)
Used to manually version bump the version number. Should be in the format `X.Y.Z`. All future pushes will subsequently start incrementing based on the new MAJOR or MINOR version changes. 
  - Ex: If the last version before triggering this workflow is `v0.0.42`, and the workflow was triggered with `v0.1.0`, the next `built.yml` workflow run will create the version tag `v0.1.1`. 

## Set up  
1. Find/Generate Unity license
    1. Open Unity Hub and log in with your Unity account (If you do not have a current Unity License) Navigate to Preferences > Licenses > Add) 
    2. Find your `Unity_lic.ulf` file
        - Windows: `C:\ProgramData\Unity\Unity_lic.ulf`
        - Mac: `/Library/Application Support/Unity/Unity_lic.ulf`
        - Linux: `~/.local/share/unity3d/Unity/Unity_lic.ulf`
2. Hook up Unity Credentials 
    1. On your GitHub repo's, navigate to Setting > Secrets and variables > Actions
    2. Create three new Repository secrets
        - `UNITY_LICENSE` (Copy the contents of your license file into here)
        - `UNITY_EMAIL` (Add the email address that you use to log into Unity)
        - `UNITY_PASSWORD` (Add the password that you use to log into Unity)
3. Create initial version tag
     1. Navigate to your GitHub version tags page `github.com/username_or_org/repo_name/releases/new`
     2. Click "Tag: Select Tag"
     3. Set tag to v0.0.0
     4. Click "Create"
     5. Set "Release title" 
4. Copy the workflows located in `.github/workflows/` into your `.github/workflows/` (create this directory if you don't have one already.
   - `build.yml`
   - `version-bump.yml`
5. In `build.yml`'s `buildForAllSupportedPlatforms` step, include the Unity Build Profiles you want generated.
6. In `build.yml`'s `Build with Unity (Build Profile)` step, set the `projectPath` variable to your project folder.
7. In `build.yml`'s `Build with Unity (Build Profile)` step, set the `unityVersion` variable to the version of Unity you're using
    - Ensure it uses a version of Unity that GameCI supports on their [tags page](https://hub.docker.com/r/unityci/editor/tags). 
8. In `build.yml`, ctrl+f to search for the string `project_name` and replace all instances of `project_name` with your game's name.
