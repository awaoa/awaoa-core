# Advantage

## Provide better information when browsing the history

Use command `$ git log <last-tag> HEAD --pretty=format:%s`can see the brief information of the history commit

For example:

```bash
[root~/angular.js]$ git log fb2d3f --pretty=format:%s |head -4

docs(guide/Components): add link to documentation for component method
docs(angular.Module): improved documentation for component method
fix(input[date]): correctly parse 2-digit years
docs(guide/migration): add notes on 1.6 to 1.7 migration
```

## Allow ignoring commits by git bisect (not important commits like formatting)

List of all subjects (first lines in commit message) since last release:

`$ git log <last tag> HEAD --pretty=format:%s`

List of some kind of subjects since last release:

`$ git log <last tag> HEAD --pretty=format:%s --grep='^<TYPE>'`

```bash
[root~/angular.js]$ git log fb2d3f --grep='^fix' --pretty=format:%s

fix(input[date]): correctly parse 2-digit years
fix(input): allow overriding timezone for date input types
fix(input): take timezone into account when validating minimum and maximum date types
fix(jqLite): make removeData() not remove event handlers
```

New features in this release:

`$ git log <last relase> HEAD --grep=feture`

```bash
[root~/angular.js]$ git log --grep=feture

commit 891acf4c201823fd2c925ee321c70d06737d5944
Author: Georgios Kalpakas <g.kalpakas@hotmail.com>
Date:   Wed Oct 29 18:48:04 2014 +0200

    fix($route): fix redirection with optional/eager params

    Previously, when (automatically) redirecting from path that fetured a
    trailing slash and optional or "eager" parameters, the resulting path
    would (incorrectly) contain the special characters (`?`,`*`) along with
    the parameter values.

    Closes #9819

    Closes #9827
```

## Allow generating CHANGELOG.md by script

We use these three sections in changelog: **new features**, **bug fixes**, **breaking changes**.This list could be generated by script when doing a release. Along with links to related commits.Of course you can edit this change log before actual release, but it could generate the skeleton.

[example_1](https://github.com/karma-runner/karma/blob/master/CHANGELOG.md)/[example_2](https://github.com/btford/grunt-conventional-changelog/blob/master/CHANGELOG.md)

```bash
6.0.0 (2016-02-11)
Bug Fixes
    concat: make sure the encoding is always buffer (75fc4ee)
    error: attach handler to the stream (a54a194)

Chores
    deps: bump (0320cba)

BREAKING CHANGES
    deps: Using conventional-changelog v1.
```