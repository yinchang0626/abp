module.exports = {
  name: 'my-project-name',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/libs/my-project-name',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
