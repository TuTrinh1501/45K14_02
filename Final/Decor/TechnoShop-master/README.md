# TechnoShop
1 - Clone project (Using Git Bash)
  >git clone https://github.com/toritrieu/TechnoShop.git
  
2 - Create new branch
  >git checkout -b [Branch Name] (Example: git checkout -b develop_ductk)
  
3 - Switch to an existed branch
  >git checkout [Branch Name]
  
4 - Push code steps:
  >Step 1: <br/>
    + Case 1: git add . -> Add all changed files (git status -> check list of added files) <br/>
    + Case 2: git add "link file" -> Add only one changed file <br/>(Example: git add "TechnoShop/Views/Home/About.cshtml")
    
  >Step 2: <br/>
    &emsp;git commit -m"Commit something here" -> Commit what has done or changed
    
  >Step 3: <br/>
    &emsp;git pull origin master -> Pull code from master to check if having CONFLICT, find CONFLICT and fix. <br/>
    &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Re-do from step 1
  
  >Step 4: <br/>
    &emsp;git push origin [Your Branch Name] -> Push your changed to github
    
  >Step 5: <br/>
    &emsp;Create merge pull request on github
    
  
