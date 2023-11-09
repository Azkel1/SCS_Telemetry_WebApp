/// <binding BeforeBuild='build' />
const { exec } = require('child_process');
const { rmdirSync, existsSync } = require('fs');
const { src, dest } = require('gulp');

function build(cb) {
    if (existsSync('views')) {
        rmdirSync('views', {
            recursive: true,
            force: true
        });
    }

    exec('cd frontend && pnpm i && pnpm build', (err, stdout, stderr) => {
        console.log("testing", err, stdout, stderr);

        src('frontend/build/**/*').pipe(dest('views'))

        console.log('Copied files to output directory (views)')

        cb(err);
    });
}

exports.build = build;