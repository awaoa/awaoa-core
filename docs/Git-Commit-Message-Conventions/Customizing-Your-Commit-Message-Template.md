# Customizing Your Commit Message Template

## Use `git config`  to customize

You can use the following bash file to create commit-message template.

```bash
#!/bin/bash
read -p "please input username:" name
read -p "please input user email:" email
echo '<type>(<scope>): <subject>' > ~/.prepare-commit-msg-temp
echo '' >> ~/.prepare-commit-msg-temp
echo '<body>' >> ~/.prepare-commit-msg-temp
echo "Author: $name" >> ~/.prepare-commit-msg-temp
echo "E-mail: $email" >>  ~/.prepare-commit-msg-temp
echo '' >> ~/.prepare-commit-msg-temp
echo '<footer>' >> ~/.prepare-commit-msg-temp
echo '' >> ~/.prepare-commit-msg-temp
echo '# HINT: Any line cannot be longer 70 characters!' >> ~/.prepare-commit-msg-temp
echo '# types: feat, fix, docs, style, refactor, test, chore(maintain)' >> ~/.prepare-commit-msg-temp
echo '' >> ~/.prepare-commit-msg-temp
echo '#-------------------------------------------------------' >> ~/.prepare-commit-msg-temp
git config --global commit.template ~/.prepare-commit-msg-temp # customizing commit template

echo "SUCCESS!"

```

Use command `git commit`to get commit-message template

```bash
<type>(<scope>): <subject>

<body>
Author: laisc
E-mail: laisc@go-pin.com

<footer>

# HINT: Any line cannot be longer 70 characters!
# types: feat, fix, docs, style, refactor, test, chore(maintain)

#-------------------------------------------------------

# change message...
```

If you want to customize your template，use command`vi ~/.prepare-commit-msg-temp`add customize hints.

## Edit `.git/hooks/prepare-commit-msg`  to customize

Edit the project's `prepare-commit-msg.sample` under `.git/hooks/`, and rename it to `prepare-commit-msg` to create a custom commit template.

```bash
[root~/angular.js]$ ls .git/hooks/
applypatch-msg.sample  post-update.sample     prepare-commit-msg.sample
commit-msg.sample      pre-applypatch.sample  pre-rebase.sample
post-commit.sample     pre-commit.sample      update.sample
post-receive.sample
```

For details, see [Customizing-Git-Git-Configuration](https://git-scm.com/book/en/v2/Customizing-Git-Git-Configuration#_code_commit_template_code)
