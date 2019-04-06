# Detail

## Format

A commit message consists of a header, a body and a footer, separated by a blank line.

Any line of the commit message cannot be longer **72 characters**!

```wiki
<type>(<scope>): <subject> (#<work-item-id>)
// BLANK-LINE
<body>
// BLANK-LINE
<footer>
```

### (1) type

> Required, This describes the kind of change that this commit is providing.

+ **build**: Changes that affect the build system or external dependencies (example scopes: gulp, broccoli, npm)
+ **ci**: Changes to our CI configuration files and scripts (example scopes: Travis, Circle, BrowserStack, SauceLabs)
+ **docs**: Documentation only changes
+ **feat**: A new feature
+ **fix**: A bug fix
+ **perf**: A code change that improves performance
+ **refactor**: A code change that neither fixes a bug nor adds a feature
+ **style**: Changes that do not affect the meaning of the code (white-space, formatting, missing semi-colons, etc)
+ **test**: Adding missing tests or correcting existing tests

### (2) scope

> Required, Scope can be anything specifying place of the commit change.

 For example $location, $browser, $compile, $rootScope, ngHref, ngClick, ngView, etc...

You can use`*` if there isn't a more fitting scope,then edit specific impact point to `<body>` ——[example](https://github.com/angular/angular.js/commit/7e5a12e2c19fe6e3d43367ba56f204a2aeda4ff4)

### (3) subject

> Required, This is a very short description of the change.

+ use imperative, present tense: “change” not “changed” nor “changes”
+ don't capitalize first letter
+ no dot `.` at the end

### (4) work-item-id

> Optional，related work-item-id.

This is useful for anyone viewing the commit history in the future to know which work items are related, no matter what tool they’re using to view history.

### (5) revert

> Special case, use it when reverts a previous commit.

Its header should begin with `revert:`, followed by the header of the reverted commit.
In the body it should say: `This reverts commit <hash>.`, where the hash is the SHA of the commit being reverted.

```bash
revert: feat(pencil): add 'graphiteWidth' option

This reverts commit 667ecc1654a317a13331b17617d973392f415f02.
```

### (6) body

> Optional，More detailed explanatory text, if necessary.  Wrap it to about 72 characters or so.

```html
More detailed explanatory text, if necessary.  Wrap it to
about 72 characters or so.

Further paragraphs come after blank lines.

- Bullet points are okay, too
- Use a hanging indent

Related work items:  # 20
```

Three points of attention:

+ Just as in `<subject>` use imperative, present tense: “change” not “changed” nor “changes”
+ Includes motivation for the change and contrasts with previous behavior
+ Wrap it to about 72 characters or so

[Linking Work Items to Git Branches, Commits, and Pull Requests](https://blogs.msdn.microsoft.com/devops/2016/03/02/linking-work-items-to-git-branches-commits-and-pull-requests/)

### (7) footer

> Optional，Only two options: **Breaking Change** and **Referencing  issue/pull request**

#### 1. Breaking changes

All breaking changes have to be mentioned as a breaking change block in the footer, which should start with the word `BREAKING CHANGE`: with a space or two newlines. The rest of the commit message is then the description of the change, justification and migration notes.

```git
BREAKING CHANGE: isolate scope bindings definition has changed.

        To migrate the code follow the example below:

        Before:

        scope: {
          myAttr: 'attribute',
        }

        After:

        scope: {
          myAttr: '@',
        }
The removed `inject` wasn't generaly useful for directives so there should be no code using it.
```

#### 2. Referencing  `issue`/`pull request`

Closed bugs should be listed on a separate line in the footer prefixed with "Closes" keyword like this:

```git
Closes #<issue-id>
```

or in case of multiple issues:

```git
Closes #<issue-id_1>, #<issue-id_2>, #<issue-id_3>
````

`pull request` equally effective:

```git
Closes #<pull-request-id>
```

If more than one `issue` is associated with a `pull request`:

```git
Closes #<pull-request-id>
Related #<issue-id_1>, #<issue-id_2>, #<issue-id_3>
```
